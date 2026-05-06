using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridge.Api.DTOs.Enrollments;
using SkillBridge.Api.Helpers;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = RoleConstants.Student)]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost("{courseId:int}")]
    public async Task<ActionResult<EnrollmentResponseDto>> EnrollInCourse(int courseId)
    {
        var enrollment = await _enrollmentService.EnrollInCourseAsync(User.GetUserId(), courseId);
        return Ok(enrollment);
    }

    [HttpGet("my-courses")]
    public async Task<ActionResult<IEnumerable<EnrollmentResponseDto>>> GetMyCourses()
    {
        var enrollments = await _enrollmentService.GetMyCoursesAsync(User.GetUserId());
        return Ok(enrollments);
    }

    [HttpPost("lessons/complete")]
    public async Task<IActionResult> MarkLessonComplete(LessonProgressDto request)
    {
        var updated = await _enrollmentService.MarkLessonCompleteAsync(User.GetUserId(), request);
        return updated ? Ok(new { message = "Lesson marked as completed." }) : NotFound();
    }
}
