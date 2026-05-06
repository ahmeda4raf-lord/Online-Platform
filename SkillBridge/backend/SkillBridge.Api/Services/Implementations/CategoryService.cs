using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillBridge.Api.Data;
using SkillBridge.Api.DTOs.Categories;
using SkillBridge.Api.Models;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync()
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
    }

    public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto request)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<CategoryResponseDto?> UpdateCategoryAsync(int categoryId, CategoryUpdateDto request)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category is null)
        {
            return null;
        }

        category.Name = request.Name;
        category.Description = request.Description;
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category is null)
        {
            return false;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}
