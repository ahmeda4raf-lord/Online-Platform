using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SkillBridge.Api.Data;
using SkillBridge.Api.DTOs.Courses;
using SkillBridge.Api.Models;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Services.Implementations;

public class CourseService : ICourseService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CourseService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseResponseDto>> GetPublishedCoursesAsync(CourseFilterDto filter)
    {
        var query = _context.Courses
            .AsNoTracking()
            .Where(course => course.Status == CourseStatus.Published)
            .Include(course => course.Category)
            .Include(course => course.Instructor)
            .Include(course => course.Sections)
                .ThenInclude(section => section.Lessons)
            .AsQueryable();

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(course => course.CategoryId == filter.CategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Level))
        {
            query = query.Where(course => course.Level == filter.Level);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(course => course.Price <= filter.MaxPrice.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(course => course.Title.Contains(filter.Search) || course.Description.Contains(filter.Search));
        }

        return await query
            .OrderByDescending(course => course.CreatedAt)
            .ProjectTo<CourseResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<CourseResponseDto?> GetCourseDetailsAsync(int courseId)
    {
        var course = await _context.Courses
            .AsNoTracking()
            .Include(course => course.Category)
            .Include(course => course.Instructor)
            .Include(course => course.Sections)
                .ThenInclude(section => section.Lessons)
            .FirstOrDefaultAsync(course => course.Id == courseId);

        return course is null ? null : _mapper.Map<CourseResponseDto>(course);
    }

    public async Task<CourseResponseDto> CreateCourseAsync(string instructorId, CourseCreateDto request)
    {
        var course = new Course
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            Level = request.Level,
            ThumbnailUrl = request.ThumbnailUrl,
            CategoryId = request.CategoryId,
            InstructorId = instructorId,
            Status = CourseStatus.Draft,
            Sections = request.Sections
                .OrderBy(section => section.Order)
                .Select(section => new CourseSection
                {
                    Title = section.Title,
                    Order = section.Order,
                    Lessons = section.Lessons
                        .OrderBy(lesson => lesson.Order)
                        .Select(lesson => new Lesson
                        {
                            Title = lesson.Title,
                            Content = lesson.Content,
                            VideoUrl = lesson.VideoUrl,
                            Order = lesson.Order,
                            IsPreview = lesson.IsPreview
                        })
                        .ToList()
                })
                .ToList()
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return await GetCourseDetailsAsync(course.Id)
            ?? throw new InvalidOperationException("Course could not be loaded after creation.");
    }

    public async Task<CourseResponseDto?> UpdateCourseAsync(int courseId, string instructorId, CourseUpdateDto request)
    {
        var course = await _context.Courses
            .Include(item => item.Sections)
                .ThenInclude(section => section.Lessons)
            .FirstOrDefaultAsync(item => item.Id == courseId && item.InstructorId == instructorId);

        if (course is null)
        {
            return null;
        }

        course.Title = request.Title;
        course.Description = request.Description;
        course.Price = request.Price;
        course.Level = request.Level;
        course.ThumbnailUrl = request.ThumbnailUrl;
        course.CategoryId = request.CategoryId;
        course.UpdatedAt = DateTime.UtcNow;
        course.RejectionReason = null;

        _context.Lessons.RemoveRange(course.Sections.SelectMany(section => section.Lessons));
        _context.CourseSections.RemoveRange(course.Sections);

        course.Sections = request.Sections
            .OrderBy(section => section.Order)
            .Select(section => new CourseSection
            {
                Title = section.Title,
                Order = section.Order,
                Lessons = section.Lessons
                    .OrderBy(lesson => lesson.Order)
                    .Select(lesson => new Lesson
                    {
                        Title = lesson.Title,
                        Content = lesson.Content,
                        VideoUrl = lesson.VideoUrl,
                        Order = lesson.Order,
                        IsPreview = lesson.IsPreview
                    })
                    .ToList()
            })
            .ToList();

        await _context.SaveChangesAsync();
        return await GetCourseDetailsAsync(courseId);
    }

    public async Task<bool> DeleteCourseAsync(int courseId, string instructorId)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(item => item.Id == courseId && item.InstructorId == instructorId);
        if (course is null)
        {
            return false;
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SubmitCourseForReviewAsync(int courseId, string instructorId)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(item => item.Id == courseId && item.InstructorId == instructorId);
        if (course is null)
        {
            return false;
        }

        course.Status = CourseStatus.Pending;
        course.RejectionReason = null;
        course.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CourseResponseDto>> GetInstructorCoursesAsync(string instructorId)
    {
        return await _context.Courses
            .AsNoTracking()
            .Where(course => course.InstructorId == instructorId)
            .Include(course => course.Category)
            .Include(course => course.Instructor)
            .Include(course => course.Sections)
                .ThenInclude(section => section.Lessons)
            .ProjectTo<CourseResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<CourseStudentDto>> GetCourseStudentsAsync(int courseId, string instructorId)
    {
        return await _context.Enrollments
            .AsNoTracking()
            .Where(enrollment => enrollment.CourseId == courseId && enrollment.Course!.InstructorId == instructorId)
            .Include(enrollment => enrollment.Student)
            .Select(enrollment => new CourseStudentDto
            {
                StudentId = enrollment.StudentId,
                FullName = enrollment.Student!.FullName,
                Email = enrollment.Student.Email ?? string.Empty,
                ProgressPercentage = enrollment.ProgressPercentage,
                IsCompleted = enrollment.IsCompleted,
                EnrolledAt = enrollment.EnrolledAt
            })
            .ToListAsync();
    }
}
