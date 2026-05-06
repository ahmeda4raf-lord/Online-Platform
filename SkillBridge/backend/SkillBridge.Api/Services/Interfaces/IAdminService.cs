using SkillBridge.Api.DTOs.Auth;
using SkillBridge.Api.DTOs.Courses;

namespace SkillBridge.Api.Services.Interfaces;

public interface IAdminService
{
    Task<IEnumerable<CourseResponseDto>> GetPendingCoursesAsync();
    Task<bool> ApproveCourseAsync(int courseId);
    Task<bool> RejectCourseAsync(int courseId, string reason);
    Task<IEnumerable<UserResponseDto>> GetUsersAsync();
    Task<bool> BlockUserAsync(string userId);
    Task<bool> UnblockUserAsync(string userId);
}
