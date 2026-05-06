using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillBridge.Api.Models;

namespace SkillBridge.Api.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseSection> CourseSections => Set<CourseSection>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<LessonProgress> LessonProgressRecords => Set<LessonProgress>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>()
            .Property(category => category.Name)
            .HasMaxLength(100);

        builder.Entity<Course>()
            .Property(course => course.Price)
            .HasColumnType("decimal(18,2)");

        builder.Entity<Course>()
            .HasOne(course => course.Category)
            .WithMany(category => category.Courses)
            .HasForeignKey(course => course.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Course>()
            .HasOne(course => course.Instructor)
            .WithMany(user => user.InstructedCourses)
            .HasForeignKey(course => course.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CourseSection>()
            .HasOne(section => section.Course)
            .WithMany(course => course.Sections)
            .HasForeignKey(section => section.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Lesson>()
            .HasOne(lesson => lesson.CourseSection)
            .WithMany(section => section.Lessons)
            .HasForeignKey(lesson => lesson.CourseSectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Enrollment>()
            .HasOne(enrollment => enrollment.Student)
            .WithMany(student => student.Enrollments)
            .HasForeignKey(enrollment => enrollment.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Enrollment>()
            .HasOne(enrollment => enrollment.Course)
            .WithMany(course => course.Enrollments)
            .HasForeignKey(enrollment => enrollment.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Enrollment>()
            .HasIndex(enrollment => new { enrollment.StudentId, enrollment.CourseId })
            .IsUnique();

        builder.Entity<LessonProgress>()
            .HasOne(progress => progress.Student)
            .WithMany(student => student.LessonProgressRecords)
            .HasForeignKey(progress => progress.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<LessonProgress>()
            .HasOne(progress => progress.Lesson)
            .WithMany(lesson => lesson.LessonProgressRecords)
            .HasForeignKey(progress => progress.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LessonProgress>()
            .HasIndex(progress => new { progress.StudentId, progress.LessonId })
            .IsUnique();

        builder.Entity<Review>()
            .HasOne(review => review.Student)
            .WithMany(student => student.Reviews)
            .HasForeignKey(review => review.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Review>()
            .HasOne(review => review.Course)
            .WithMany(course => course.Reviews)
            .HasForeignKey(review => review.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Review>()
            .HasIndex(review => new { review.StudentId, review.CourseId })
            .IsUnique();
    }
}
