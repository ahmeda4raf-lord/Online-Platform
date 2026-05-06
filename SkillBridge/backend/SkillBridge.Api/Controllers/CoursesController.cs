using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridge.Api.DTOs.Courses;
using SkillBridge.Api.Helpers;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet("published")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetPublishedCourses([FromQuery] CourseFilterDto filter)
    {
        var courses = await _courseService.GetPublishedCoursesAsync(filter);
        return Ok(courses);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<CourseResponseDto>> GetCourseDetails(int id)
    {
        var course = await _courseService.GetCourseDetailsAsync(id);
        return course is null ? NotFound() : Ok(course);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Instructor)]
    public async Task<ActionResult<CourseResponseDto>> CreateCourse(CourseCreateDto request)
    {
        var course = await _courseService.CreateCourseAsync(User.GetUserId(), request);
        return CreatedAtAction(nameof(GetCourseDetails), new { id = course.Id }, course);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = RoleConstants.Instructor)]
    public async Task<ActionResult<CourseResponseDto>> UpdateCourse(int id, CourseUpdateDto request)
    {
        var course = await _courseService.UpdateCourseAsync(id, User.GetUserId(), request);
        return course is null ? NotFound() : Ok(course);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = RoleConstants.Instructor)]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var deleted = await _courseService.DeleteCourseAsync(id, User.GetUserId());
        return deleted ? NoContent() : NotFound();
    }

    [HttpPost("{id:int}/submit")]
    [Authorize(Roles = RoleConstants.Instructor)]
    public async Task<IActionResult> SubmitCourseForReview(int id)
    {
        var submitted = await _courseService.SubmitCourseForReviewAsync(id, User.GetUserId());
        return submitted ? Ok(new { message = "Course submitted for admin review." }) : NotFound();
    }
}
