using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SkillBridge.Api.DTOs.Auth;
using SkillBridge.Api.Helpers;
using SkillBridge.Api.Models;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        var normalizedRole = NormalizeRole(request.Role);
        var user = new ApplicationUser
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            ThrowIdentityError(result);
        }

        await _userManager.AddToRoleAsync(user, normalizedRole);
        return await BuildAuthResponseAsync(user, normalizedRole);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new UnauthorizedAccessException("Invalid email or password.");

        if (user.IsBlocked)
        {
            throw new UnauthorizedAccessException("Your account has been blocked.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? RoleConstants.Student;
        return await BuildAuthResponseAsync(user, role);
    }

    private async Task<AuthResponseDto> BuildAuthResponseAsync(ApplicationUser user, string role)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email ?? string.Empty,
            Role = role,
            ExpiresAt = expiresAt
        };
    }

    private static string NormalizeRole(string role)
    {
        var normalizedRole = role?.Trim() ?? RoleConstants.Student;

        if (string.Equals(normalizedRole, RoleConstants.Instructor, StringComparison.OrdinalIgnoreCase))
        {
            return RoleConstants.Instructor;
        }

        return RoleConstants.Student;
    }

    private static void ThrowIdentityError(IdentityResult result)
    {
        var errors = string.Join(", ", result.Errors.Select(error => error.Description));
        throw new InvalidOperationException(errors);
    }
}
