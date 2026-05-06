using Microsoft.EntityFrameworkCore;
using SkillBridge.Api.Data;
using SkillBridge.Api.DTOs.Reviews;
using SkillBridge.Api.Models;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Services.Implementations;

public class ReviewService : IReviewService
{
    private readonly AppDbContext _context;

    public ReviewService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReviewResponseDto> AddReviewAsync(string studentId, ReviewCreateDto request)
    {
        var review = await _context.Reviews
            .Include(item => item.Student)
            .FirstOrDefaultAsync(item => item.StudentId == studentId && item.CourseId == request.CourseId);

        if (review is null)
        {
            review = new Review
            {
                StudentId = studentId,
                CourseId = request.CourseId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
        }
        else
        {
            review.Rating = request.Rating;
            review.Comment = request.Comment;
        }

        await _context.SaveChangesAsync();

        review = await _context.Reviews
            .AsNoTracking()
            .Include(item => item.Student)
            .FirstAsync(item => item.Id == review.Id);

        return MapReview(review);
    }

    public async Task<ReviewResponseDto?> UpdateReviewAsync(int reviewId, string studentId, ReviewUpdateDto request)
    {
        var review = await _context.Reviews
            .Include(item => item.Student)
            .FirstOrDefaultAsync(item => item.Id == reviewId && item.StudentId == studentId);

        if (review is null)
        {
            return null;
        }

        review.Rating = request.Rating;
        review.Comment = request.Comment;
        await _context.SaveChangesAsync();
        return MapReview(review);
    }

    public async Task<bool> DeleteReviewAsync(int reviewId, string studentId)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(item => item.Id == reviewId && item.StudentId == studentId);
        if (review is null)
        {
            return false;
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return true;
    }

    private static ReviewResponseDto MapReview(Review review)
    {
        return new ReviewResponseDto
        {
            Id = review.Id,
            CourseId = review.CourseId,
            StudentId = review.StudentId,
            StudentName = review.Student?.FullName ?? string.Empty,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt
        };
    }
}
