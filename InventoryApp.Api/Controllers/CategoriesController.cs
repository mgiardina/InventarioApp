using InventoryApp.Api.Models;
using InventoryApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IResult> GetCategories()
    {
        var categories = await _categoryService.GetAllAsync();
        return Results.Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetCategory(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return category != null ? Results.Ok(category) : Results.NotFound();
    }

    [HttpPost]
    public async Task<IResult> CreateCategory(CategoryDto categoryDto)
    {
        var createdCategory = await _categoryService.CreateAsync(categoryDto);
        return Results.Created($"/api/categories/{createdCategory.CategoryID}", createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateCategory(int id, CategoryDto categoryDto)
    {
        if (id != categoryDto.CategoryID) return Results.BadRequest();
        var success = await _categoryService.UpdateAsync(categoryDto);
        return success ? Results.NoContent() : Results.NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteCategory(int id)
    {
        var success = await _categoryService.DeleteAsync(id);
        return success ? Results.NoContent() : Results.NotFound();
    }
}
