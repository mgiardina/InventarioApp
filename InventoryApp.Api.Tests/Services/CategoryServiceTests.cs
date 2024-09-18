using AutoMapper;
using InventoryApp.Api.Configuration;
using InventoryApp.Api.Models;
using InventoryApp.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Api.Tests.Services;

public class CategoryServiceTests
{
    private readonly ApplicationDbContext _fakeDbContext;
    private readonly IMapper _fakeMapper;
    private readonly ICategoryService _categoryService;

    public CategoryServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _fakeDbContext = new ApplicationDbContext(options);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _fakeMapper = config.CreateMapper();

        _categoryService = new CategoryService(_fakeDbContext, _fakeMapper);
    }

    public void Dispose()
    {
        _fakeDbContext.Database.EnsureDeleted();
        _fakeDbContext.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfCategories()
    {
        // Arrange
        _fakeDbContext.Categories.Add(new Category { CategoryID = 1, Name = "Electronics" });
        _fakeDbContext.Categories.Add(new Category { CategoryID = 2, Name = "Books" });
        await _fakeDbContext.SaveChangesAsync();

        // Act
        var result = await _categoryService.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCategory_WhenCategoryExists()
    {
        // Arrange
        var category = new Category { CategoryID = 1, Name = "Electronics" };
        _fakeDbContext.Categories.Add(category);
        await _fakeDbContext.SaveChangesAsync();

        // Act
        var result = await _categoryService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Electronics", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenCategoryDoesNotExist()
    {
        // Act
        var result = await _categoryService.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_AddsCategoryToDatabase()
    {
        // Arrange
        var newCategoryDto = new CategoryDto { Name = "Furniture" };

        // Act
        var result = await _categoryService.CreateAsync(newCategoryDto);

        // Assert
        var categoryInDb = await _fakeDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryID == result.CategoryID);
        Assert.NotNull(categoryInDb);
        Assert.Equal("Furniture", categoryInDb.Name);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenUpdateIsSuccessful()
    {
        // Arrange
        var category = new Category { CategoryID = 1, Name = "Electronics" };
        _fakeDbContext.Categories.Add(category);
        await _fakeDbContext.SaveChangesAsync();

        var updatedCategoryDto = new CategoryDto { CategoryID = 1, Name = "Updated Electronics" };

        // Act
        var result = await _categoryService.UpdateAsync(updatedCategoryDto);

        // Assert
        Assert.True(result);

        var categoryInDb = await _fakeDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryID == 1);
        Assert.Equal("Updated Electronics", categoryInDb.Name);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenCategoryDoesNotExist()
    {
        // Arrange
        var updatedCategoryDto = new CategoryDto { CategoryID = 999, Name = "Non-existent Category" };

        // Act
        var result = await _categoryService.UpdateAsync(updatedCategoryDto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenDeleteIsSuccessful()
    {
        // Arrange
        var category = new Category { CategoryID = 1, Name = "Electronics" };
        _fakeDbContext.Categories.Add(category);
        await _fakeDbContext.SaveChangesAsync();

        // Act
        var result = await _categoryService.DeleteAsync(1);

        // Assert
        Assert.True(result);

        var categoryInDb = await _fakeDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryID == 1);
        Assert.Null(categoryInDb);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenCategoryDoesNotExist()
    {
        // Act
        var result = await _categoryService.DeleteAsync(999);

        // Assert
        Assert.False(result);
    }
}
