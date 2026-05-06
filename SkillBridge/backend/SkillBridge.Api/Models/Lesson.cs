namespace SkillBridge.Api.Models;

public class Lesson
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsPreview { get; set; }
    public int CourseSectionId { get; set; }

    public CourseSection? CourseSection { get; set; }
    public ICollection<LessonProgress> LessonProgressRecords { get; set; } = new List<LessonProgress>();
}
