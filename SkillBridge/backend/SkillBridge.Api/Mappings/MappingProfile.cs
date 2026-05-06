using AutoMapper;
using SkillBridge.Api.DTOs.Categories;
using SkillBridge.Api.DTOs.Courses;
using SkillBridge.Api.Models;

namespace SkillBridge.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryResponseDto>();
        CreateMap<Lesson, LessonResponseDto>();
        CreateMap<CourseSection, SectionResponseDto>();
        CreateMap<Course, CourseResponseDto>()
            .ForMember(destination => destination.Status, options => options.MapFrom(source => source.Status.ToString()))
            .ForMember(destination => destination.InstructorName, options => options.MapFrom(source => source.Instructor != null ? source.Instructor.FullName : string.Empty))
            .ForMember(destination => destination.CategoryName, options => options.MapFrom(source => source.Category != null ? source.Category.Name : string.Empty));
    }
}
