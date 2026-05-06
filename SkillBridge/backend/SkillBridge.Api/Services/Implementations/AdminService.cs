using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillBridge.Api.Data;
using SkillBridge.Api.DTOs.Auth;
using SkillBridge.Api.DTOs.Courses;
using SkillBridge.Api.Models;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Services.Implementations;

public class AdminService : IAdminService
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICourseService _courseService;

    public AdminService(AppDbContext context, UserManager<ApplicationUser> userManager, ICourseService courseService)
    {
        _context = context;
        _userManager = userManager;
        _courseService = courseService;
    }

    public async Task<IEnumerable<CourseResponseDto>> GetPendingCoursesAsync()
    {
        var courseIds = await _context.Courses
            .AsNoTracking()
            .Where(course => course.Status == CourseStatus.Pending)
            .OrderBy(course => course.CreatedAt)
            .Select(course => course.Id)
            .ToListAsync();

        var courses = new List<CourseResponseDto>();

        foreach (var courseId in courseIds)
        {
            var course = await _courseService.GetCourseDetailsAsync(courseId);
            if (course is not null)
            {
                courses.Add(course);
            }
        }

        return courses;
    }

    public async Task<bool> ApproveCourseAsync(int courseId)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(item => item.Id == courseId);
        if (course is null)
        {
            return false;
        }

        course.Status = CourseStatus.Published;
        course.RejectionReason = null;
        course.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RejectCourseAsync(int courseId, string reason)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(item => item.Id == courseId);
        if (course is null)
        {
            return false;
        }

        course.Status = CourseStatus.Rejected;
        course.RejectionReason = reason;
        course.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
    {
        var users = await _userManager.Users.AsNoTracking().OrderBy(user => user.CreatedAt).ToListAsync();
        var result = new List<UserResponseDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            result.Add(new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                Role = roles.FirstOrDefault() ?? string.Empty,
                IsBlocked = user.IsBlocked,
                CreatedAt = user.CreatedAt
            });
        }

        return result;
    }

    public async Task<bool> BlockUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return false;
        }

        user.IsBlocked = true;
        await _userManager.UpdateAsync(user);
        return true;
    }

    public async Task<bool> UnblockUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return false;
        }

        user.IsBlocked = false;
        await _userManager.UpdateAsync(user);
        return true;
    }
}
