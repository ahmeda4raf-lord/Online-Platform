namespace SkillBridge.Api.Models;

public class LessonProgress
{
    public int Id { get; set; }
    public string StudentId { get; set; } = string.Empty;
    public int LessonId { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }

    public ApplicationUser? Student { get; set; }
    public Lesson? Lesson { get; set; }
}
