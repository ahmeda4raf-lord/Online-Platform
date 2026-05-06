namespace SkillBridge.Api.Models;

public class Enrollment
{
    public int Id { get; set; }
    public string StudentId { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    public double ProgressPercentage { get; set; }
    public bool IsCompleted { get; set; }

    public ApplicationUser? Student { get; set; }
    public Course? Course { get; set; }
}
