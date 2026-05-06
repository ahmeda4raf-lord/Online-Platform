using SkillBridge.Api.DTOs.Courses;

namespace SkillBridge.Api.Services.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<CourseResponseDto>> GetPublishedCoursesAsync(CourseFilterDto filter);
    Task<CourseResponseDto?> GetCourseDetailsAsync(int courseId);
    Task<CourseResponseDto> CreateCourseAsync(string instructorId, CourseCreateDto request);
    Task<CourseResponseDto?> UpdateCourseAsync(int courseId, string instructorId, CourseUpdateDto request);
    Task<bool> DeleteCourseAsync(int courseId, string instructorId);
    Task<bool> SubmitCourseForReviewAsync(int courseId, string instructorId);
    Task<IEnumerable<CourseResponseDto>> GetInstructorCoursesAsync(string instructorId);
    Task<IEnumerable<CourseStudentDto>> GetCourseStudentsAsync(int courseId, string instructorId);
}
