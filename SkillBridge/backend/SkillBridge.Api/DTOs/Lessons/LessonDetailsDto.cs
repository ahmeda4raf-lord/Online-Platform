namespace SkillBridge.Api.DTOs.Lessons;

public class LessonDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsPreview { get; set; }
}
