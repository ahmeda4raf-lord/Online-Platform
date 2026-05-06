namespace SkillBridge.Api.DTOs.Courses;

public class LessonCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsPreview { get; set; }
}
