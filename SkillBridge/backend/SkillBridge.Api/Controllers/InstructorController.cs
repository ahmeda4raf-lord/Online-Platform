using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridge.Api.DTOs.Courses;
using SkillBridge.Api.Helpers;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = RoleConstants.Instructor)]
public class InstructorController : ControllerBase
{
    private readonly ICourseService _courseService;

    public InstructorController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet("my-courses")]
    public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetMyCourses()
    {
        var courses = await _courseService.GetInstructorCoursesAsync(User.GetUserId());
        return Ok(courses);
    }

    [HttpGet("courses/{courseId:int}/students")]
    public async Task<ActionResult<IEnumerable<CourseStudentDto>>> GetCourseStudents(int courseId)
    {
        var students = await _courseService.GetCourseStudentsAsync(courseId, User.GetUserId());
        return Ok(students);
    }
}
