namespace SkillBridge.Api.DTOs.Reviews;

public class ReviewCreateDto
{
    public int CourseId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}
