using SkillBridge.Api.DTOs.Categories;

namespace SkillBridge.Api.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync();
    Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto request);
    Task<CategoryResponseDto?> UpdateCategoryAsync(int categoryId, CategoryUpdateDto request);
    Task<bool> DeleteCategoryAsync(int categoryId);
}
