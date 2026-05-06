using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridge.Api.DTOs.Categories;
using SkillBridge.Api.Helpers;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return Ok(categories);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult<CategoryResponseDto>> CreateCategory(CategoryCreateDto request)
    {
        var category = await _categoryService.CreateCategoryAsync(request);
        return Ok(category);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult<CategoryResponseDto>> UpdateCategory(int id, CategoryUpdateDto request)
    {
        var category = await _categoryService.UpdateCategoryAsync(id, request);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var deleted = await _categoryService.DeleteCategoryAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
