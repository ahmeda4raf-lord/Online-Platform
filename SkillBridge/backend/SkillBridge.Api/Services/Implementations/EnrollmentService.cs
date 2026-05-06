using Microsoft.EntityFrameworkCore;
using SkillBridge.Api.Data;
using SkillBridge.Api.DTOs.Enrollments;
using SkillBridge.Api.Models;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Services.Implementations;

public class EnrollmentService : IEnrollmentService
{
    private readonly AppDbContext _context;

    public EnrollmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EnrollmentResponseDto> EnrollInCourseAsync(string studentId, int courseId)
    {
        var existingEnrollment = await _context.Enrollments
            .Include(enrollment => enrollment.Course)
            .FirstOrDefaultAsync(enrollment => enrollment.StudentId == studentId && enrollment.CourseId == courseId);

        if (existingEnrollment is not null)
        {
            return MapEnrollment(existingEnrollment);
        }

        var course = await _context.Courses.FirstOrDefaultAsync(item => item.Id == courseId && item.Status == CourseStatus.Published)
            ?? throw new KeyNotFoundException("Published course not found.");

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        enrollment.Course = course;
        return MapEnrollment(enrollment);
    }

    public async Task<IEnumerable<EnrollmentResponseDto>> GetMyCoursesAsync(string studentId)
    {
        return await _context.Enrollments
            .AsNoTracking()
            .Where(enrollment => enrollment.StudentId == studentId)
            .Include(enrollment => enrollment.Course)
            .Select(enrollment => MapEnrollment(enrollment))
            .ToListAsync();
    }

    public async Task<bool> MarkLessonCompleteAsync(string studentId, LessonProgressDto request)
    {
        var lesson = await _context.Lessons
            .Include(item => item.CourseSection)
            .FirstOrDefaultAsync(item => item.Id == request.LessonId);

        if (lesson is null)
        {
            return false;
        }

        var courseSection = lesson.CourseSection;
        if (courseSection is null)
        {
            return false;
        }

        var progress = await _context.LessonProgressRecords
            .FirstOrDefaultAsync(item => item.StudentId == studentId && item.LessonId == request.LessonId);

        if (progress is null)
        {
            progress = new LessonProgress
            {
                StudentId = studentId,
                LessonId = request.LessonId,
                IsCompleted = true,
                CompletedAt = DateTime.UtcNow
            };
            _context.LessonProgressRecords.Add(progress);
        }
        else
        {
            progress.IsCompleted = true;
            progress.CompletedAt = DateTime.UtcNow;
        }

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(item => item.StudentId == studentId && item.CourseId == courseSection.CourseId);

        if (enrollment is not null)
        {
            var courseId = courseSection.CourseId;
            var totalLessons = await _context.Lessons.CountAsync(item => item.CourseSection != null && item.CourseSection.CourseId == courseId);
            var completedLessons = await _context.LessonProgressRecords.CountAsync(item =>
                item.StudentId == studentId &&
                item.IsCompleted &&
                item.Lesson != null &&
                item.Lesson.CourseSection != null &&
                item.Lesson.CourseSection.CourseId == courseId);

            enrollment.ProgressPercentage = totalLessons == 0 ? 0 : Math.Round((double)completedLessons / totalLessons * 100, 2);
            enrollment.IsCompleted = totalLessons > 0 && completedLessons == totalLessons;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    private static EnrollmentResponseDto MapEnrollment(Enrollment enrollment)
    {
        return new EnrollmentResponseDto
        {
            Id = enrollment.Id,
            CourseId = enrollment.CourseId,
            CourseTitle = enrollment.Course?.Title ?? string.Empty,
            ThumbnailUrl = enrollment.Course?.ThumbnailUrl ?? string.Empty,
            EnrolledAt = enrollment.EnrolledAt,
            ProgressPercentage = enrollment.ProgressPercentage,
            IsCompleted = enrollment.IsCompleted
        };
    }
}
