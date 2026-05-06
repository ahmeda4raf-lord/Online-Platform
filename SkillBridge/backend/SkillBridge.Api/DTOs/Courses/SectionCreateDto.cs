namespace SkillBridge.Api.DTOs.Courses;

public class SectionCreateDto
{
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<LessonCreateDto> Lessons { get; set; } = [];
}
