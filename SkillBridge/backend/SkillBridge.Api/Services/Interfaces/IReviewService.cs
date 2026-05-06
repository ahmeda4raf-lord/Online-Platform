using SkillBridge.Api.DTOs.Reviews;

namespace SkillBridge.Api.Services.Interfaces;

public interface IReviewService
{
    Task<ReviewResponseDto> AddReviewAsync(string studentId, ReviewCreateDto request);
    Task<ReviewResponseDto?> UpdateReviewAsync(int reviewId, string studentId, ReviewUpdateDto request);
    Task<bool> DeleteReviewAsync(int reviewId, string studentId);
}
