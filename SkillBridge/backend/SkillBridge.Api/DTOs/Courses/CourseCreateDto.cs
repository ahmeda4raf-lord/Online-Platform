namespace SkillBridge.Api.DTOs.Courses;

public class CourseCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Level { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public List<SectionCreateDto> Sections { get; set; } = [];
}
