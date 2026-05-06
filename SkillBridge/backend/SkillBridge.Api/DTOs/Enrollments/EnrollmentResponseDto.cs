namespace SkillBridge.Api.DTOs.Enrollments;

public class EnrollmentResponseDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public DateTime EnrolledAt { get; set; }
    public double ProgressPercentage { get; set; }
    public bool IsCompleted { get; set; }
}
