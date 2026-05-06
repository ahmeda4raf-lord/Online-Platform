using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridge.Api.DTOs.Auth;
using SkillBridge.Api.DTOs.Courses;
using SkillBridge.Api.Helpers;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = RoleConstants.Admin)]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("pending-courses")]
    public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetPendingCourses()
    {
        var courses = await _adminService.GetPendingCoursesAsync();
        return Ok(courses);
    }

    [HttpPost("courses/{courseId:int}/approve")]
    public async Task<IActionResult> ApproveCourse(int courseId)
    {
        var approved = await _adminService.ApproveCourseAsync(courseId);
        return approved ? Ok(new { message = "Course approved successfully." }) : NotFound();
    }

    [HttpPost("courses/{courseId:int}/reject")]
    public async Task<IActionResult> RejectCourse(int courseId, RejectCourseDto request)
    {
        var rejected = await _adminService.RejectCourseAsync(courseId, request.Reason);
        return rejected ? Ok(new { message = "Course rejected successfully." }) : NotFound();
    }

    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
    {
        var users = await _adminService.GetUsersAsync();
        return Ok(users);
    }

    [HttpPost("users/{userId}/block")]
    public async Task<IActionResult> BlockUser(string userId)
    {
        var blocked = await _adminService.BlockUserAsync(userId);
        return blocked ? Ok(new { message = "User blocked successfully." }) : NotFound();
    }

    [HttpPost("users/{userId}/unblock")]
    public async Task<IActionResult> UnblockUser(string userId)
    {
        var unblocked = await _adminService.UnblockUserAsync(userId);
        return unblocked ? Ok(new { message = "User unblocked successfully." }) : NotFound();
    }
}
