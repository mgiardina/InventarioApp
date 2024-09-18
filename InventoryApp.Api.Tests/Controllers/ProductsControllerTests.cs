using Microsoft.AspNetCore.Http.HttpResults;
using InventoryApp.Api.Services;
using InventoryApp.Api.Models;

namespace InventoryApp.Api.Tests.Controllers;

public class ProductsControllerTests
{
    private readonly IProductService _fakeProductService;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _fakeProductService = A.Fake<IProductService>();
        _controller = new ProductsController(_fakeProductService);
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
    {
        // Arrange
        var fakeProducts = new List<ProductDto>
        {
            new ProductDto { ProductID = 1, Name = "Laptop", Description = "High-end laptop", Image = "laptop.png" },
            new ProductDto { ProductID = 2, Name = "Smartphone", Description = "Latest model", Image = "smartphone.png" }
        };
        A.CallTo(() => _fakeProductService.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ProductDto>>(fakeProducts));

        // Act
        var result = await _controller.GetProducts();

        // Assert
        var okResult = Assert.IsType<Ok<IEnumerable<ProductDto>>>(result);
        var returnValue = Assert.IsType<List<ProductDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        A.CallTo(() => _fakeProductService.GetByIdAsync(999)).Returns(Task.FromResult<ProductDto>(null));

        // Act
        var result = await _controller.GetProduct(999);

        // Assert
        var notFoundResult = Assert.IsType<NotFound>(result);
    }

    [Fact]
    public async Task CreateProduct_ReturnsCreatedResult_WithCreatedProduct()
    {
        // Arrange
        var newProduct = new ProductDto { Name = "Tablet", Description = "New tablet", Image = "tablet.png" };
        var createdProduct = new ProductDto { ProductID = 3, Name = "Tablet", Description = "New tablet", Image = "tablet.png" };
        A.CallTo(() => _fakeProductService.CreateAsync(newProduct)).Returns(Task.FromResult(createdProduct));

        // Act
        var result = await _controller.CreateProduct(newProduct);

        // Assert
        var createdResult = Assert.IsType<Created<ProductDto>>(result);
        Assert.Equal($"/api/products/{createdProduct.ProductID}", createdResult.Location);
        var returnValue = Assert.IsType<ProductDto>(createdResult.Value);
        Assert.Equal(3, returnValue.ProductID);
        Assert.Equal("Tablet", returnValue.Name);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var updatedProduct = new ProductDto { ProductID = 1, Name = "Updated Laptop", Description = "Updated description", Image = "laptop.png" };
        A.CallTo(() => _fakeProductService.UpdateAsync(updatedProduct)).Returns(Task.FromResult(true));

        // Act
        var result = await _controller.UpdateProduct(1, updatedProduct);

        // Assert
        var noContentResult = Assert.IsType<NoContent>(result);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var updatedProduct = new ProductDto { ProductID = 1, Name = "Non-existent product", Description = "Non-existent description", Image = "nonexistent.png" };
        A.CallTo(() => _fakeProductService.UpdateAsync(updatedProduct)).Returns(Task.FromResult(false));

        // Act
        var result = await _controller.UpdateProduct(1, updatedProduct);

        // Assert
        var notFoundResult = Assert.IsType<NotFound>(result);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsBadRequest_WhenProductIdDoesNotMatch()
    {
        // Arrange
        var updatedProduct = new ProductDto { ProductID = 2, Name = "Mismatched ID", Description = "Description", Image = "mismatch.png" };

        // Act
        var result = await _controller.UpdateProduct(1, updatedProduct);

        // Assert
        var badRequestResult = Assert.IsType<BadRequest>(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        A.CallTo(() => _fakeProductService.DeleteAsync(1)).Returns(Task.FromResult(true));

        // Act
        var result = await _controller.DeleteProduct(1);

        // Assert
        var noContentResult = Assert.IsType<NoContent>(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        A.CallTo(() => _fakeProductService.DeleteAsync(1)).Returns(Task.FromResult(false));

        // Act
        var result = await _controller.DeleteProduct(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFound>(result);
    }
}
