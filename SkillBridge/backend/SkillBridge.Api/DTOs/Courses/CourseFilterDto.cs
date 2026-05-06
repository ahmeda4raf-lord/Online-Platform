namespace SkillBridge.Api.DTOs.Courses;

public class CourseFilterDto
{
    public int? CategoryId { get; set; }
    public string? Level { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Search { get; set; }
}
