namespace SkillBridge.Api.DTOs.Courses;

public class SectionResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<LessonResponseDto> Lessons { get; set; } = [];
}
