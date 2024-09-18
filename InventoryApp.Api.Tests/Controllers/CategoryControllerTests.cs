using InventoryApp.Api.Models;
using InventoryApp.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InventoryApp.Api.Tests.Controllers;

public class CategoriesControllerTests
{
    private readonly ICategoryService _fakeCategoryService;
    private readonly CategoriesController _controller;

    public CategoriesControllerTests()
    {
        _fakeCategoryService = A.Fake<ICategoryService>();
        _controller = new CategoriesController(_fakeCategoryService);
    }

    [Fact]
    public async Task GetCategories_ReturnsOkResult_WithListOfCategories()
    {
        // Arrange
        var fakeCategories = new List<CategoryDto>
        {
            new CategoryDto { CategoryID = 1, Name = "Electronics" },
            new CategoryDto { CategoryID = 2, Name = "Books" }
        };
        A.CallTo(() => _fakeCategoryService.GetAllAsync()).Returns(Task.FromResult<IEnumerable<CategoryDto>>(fakeCategories));

        // Act
        var result = await _controller.GetCategories();

        // Assert
        var okResult = Assert.IsType<Ok<IEnumerable<CategoryDto>>>(result);
        var returnValue = Assert.IsType<List<CategoryDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        A.CallTo(() => _fakeCategoryService.GetByIdAsync(999)).Returns(Task.FromResult<CategoryDto>(null));

        // Act
        var result = await _controller.GetCategory(999);

        // Assert
        var notFoundResult = Assert.IsType<NotFound>(result);
    }

    [Fact]
    public async Task CreateCategory_ReturnsCreatedResult_WithCreatedCategory()
    {
        // Arrange
        var newCategory = new CategoryDto { Name = "Furniture" };
        var createdCategory = new CategoryDto { CategoryID = 3, Name = "Furniture" };

        A.CallTo(() => _fakeCategoryService.CreateAsync(newCategory)).Returns(Task.FromResult(createdCategory));

        // Act
        var result = await _controller.CreateCategory(newCategory);

        // Assert
        var createdResult = Assert.IsType<Created<CategoryDto>>(result);
        Assert.Equal($"/api/categories/{createdCategory.CategoryID}", createdResult.Location);
        var returnValue = Assert.IsType<CategoryDto>(createdResult.Value);
        Assert.Equal(3, returnValue.CategoryID);
        Assert.Equal("Furniture", returnValue.Name);
    }

    [Fact]
    public async Task UpdateCategory_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var updatedCategory = new CategoryDto { CategoryID = 1, Name = "Updated Electronics" };

        A.CallTo(() => _fakeCategoryService.UpdateAsync(updatedCategory)).Returns(Task.FromResult(true));

        // Act
        var result = await _controller.UpdateCategory(1, updatedCategory);

        // Assert
        var noContentResult = Assert.IsType<NoContent>(result);
    }

    [Fact]
    public async Task UpdateCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var updatedCategory = new CategoryDto { CategoryID = 1, Name = "Non-existent Category" };

        A.CallTo(() => _fakeCategoryService.UpdateAsync(updatedCategory)).Returns(Task.FromResult(false));

        // Act
        var result = await _controller.UpdateCategory(1, updatedCategory);

        // Assert
        var notFoundResult = Assert.IsType<NotFound>(result);
    }

    [Fact]
    public async Task UpdateCategory_ReturnsBadRequest_WhenCategoryIdDoesNotMatch()
    {
        // Arrange
        var updatedCategory = new CategoryDto { CategoryID = 2, Name = "Mismatched ID" };

        // Act
        var result = await _controller.UpdateCategory(1, updatedCategory);

        // Assert
        var badRequestResult = Assert.IsType<BadRequest>(result);
    }

    [Fact]
    public async Task DeleteCategory_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        A.CallTo(() => _fakeCategoryService.DeleteAsync(1)).Returns(Task.FromResult(true));

        // Act
        var result = await _controller.DeleteCategory(1);

        // Assert
        var noContentResult = Assert.IsType<NoContent>(result);
    }

    [Fact]
    public async Task DeleteCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        A.CallTo(() => _fakeCategoryService.DeleteAsync(1)).Returns(Task.FromResult(false));

        // Act
        var result = await _controller.DeleteCategory(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFound>(result);
    }
}
