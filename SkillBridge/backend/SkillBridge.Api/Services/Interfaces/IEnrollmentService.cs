using SkillBridge.Api.DTOs.Enrollments;

namespace SkillBridge.Api.Services.Interfaces;

public interface IEnrollmentService
{
    Task<EnrollmentResponseDto> EnrollInCourseAsync(string studentId, int courseId);
    Task<IEnumerable<EnrollmentResponseDto>> GetMyCoursesAsync(string studentId);
    Task<bool> MarkLessonCompleteAsync(string studentId, LessonProgressDto request);
}
