using AutoMapper;
using InventoryApp.Api.Configuration;
using InventoryApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Api.Services;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<CategoryDto> CreateAsync(CategoryDto categoryDto);
    Task<bool> UpdateAsync(CategoryDto categoryDto);
    Task<bool> DeleteAsync(int id);
}

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return null;
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<bool> UpdateAsync(CategoryDto categoryDto)
    {
        var category = await _context.Categories.FindAsync(categoryDto.CategoryID);
        if (category == null) return false;

        _mapper.Map(categoryDto, category);
        _context.Categories.Update(category);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _context.Categories.Remove(category);
        return await _context.SaveChangesAsync() > 0;
    }
}

