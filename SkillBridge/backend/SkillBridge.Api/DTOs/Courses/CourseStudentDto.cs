namespace SkillBridge.Api.DTOs.Courses;

public class CourseStudentDto
{
    public string StudentId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public double ProgressPercentage { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime EnrolledAt { get; set; }
}
