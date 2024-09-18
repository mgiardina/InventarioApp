using AutoMapper;
using InventoryApp.Api.Configuration;
using InventoryApp.Api.Models;
using InventoryApp.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Api.Tests.Services;

public class ProductServiceTests : IDisposable
{
    private readonly ApplicationDbContext _fakeDbContext;
    private readonly IMapper _fakeMapper;
    private readonly IProductService _productService;

    public ProductServiceTests()
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

        _productService = new ProductService(_fakeDbContext, _fakeMapper);
    }

    public void Dispose()
    {
        _fakeDbContext.Database.EnsureDeleted();
        _fakeDbContext.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfProducts()
    {
        // Arrange
        _fakeDbContext.Products.Add(new Product { ProductID = 1, Name = "Laptop" });
        _fakeDbContext.Products.Add(new Product { ProductID = 2, Name = "Smartphone" });
        await _fakeDbContext.SaveChangesAsync();

        // Act
        var result = await _productService.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product { ProductID = 1, Name = "Laptop" };
        _fakeDbContext.Products.Add(product);
        await _fakeDbContext.SaveChangesAsync();

        // Act
        var result = await _productService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Laptop", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        // Act
        var result = await _productService.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_AddsProductToDatabase()
    {
        // Arrange
        var newProductDto = new ProductDto { Name = "Tablet", Description = "New tablet", Image = "tablet.png" };

        // Act
        var result = await _productService.CreateAsync(newProductDto);

        // Assert
        var productInDb = await _fakeDbContext.Products.FirstOrDefaultAsync(p => p.ProductID == result.ProductID);
        Assert.NotNull(productInDb);
        Assert.Equal("Tablet", productInDb.Name);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenUpdateIsSuccessful()
    {
        // Arrange
        var product = new Product { ProductID = 1, Name = "Laptop" };
        _fakeDbContext.Products.Add(product);
        await _fakeDbContext.SaveChangesAsync();

        var updatedProductDto = new ProductDto { ProductID = 1, Name = "Updated Laptop", Description = "Updated description", Image = "laptop.png" };

        // Act
        var result = await _productService.UpdateAsync(updatedProductDto);

        // Assert
        Assert.True(result);

        var productInDb = await _fakeDbContext.Products.FirstOrDefaultAsync(p => p.ProductID == 1);
        Assert.Equal("Updated Laptop", productInDb.Name);
        Assert.Equal("Updated description", productInDb.Description);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenProductDoesNotExist()
    {
        // Arrange
        var updatedProductDto = new ProductDto { ProductID = 999, Name = "Non-existent Product" };

        // Act
        var result = await _productService.UpdateAsync(updatedProductDto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenDeleteIsSuccessful()
    {
        // Arrange
        var product = new Product { ProductID = 1, Name = "Laptop" };
        _fakeDbContext.Products.Add(product);
        await _fakeDbContext.SaveChangesAsync();

        // Act
        var result = await _productService.DeleteAsync(1);

        // Assert
        Assert.True(result);

        var productInDb = await _fakeDbContext.Products.FirstOrDefaultAsync(p => p.ProductID == 1);
        Assert.Null(productInDb);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenProductDoesNotExist()
    {
        // Act
        var result = await _productService.DeleteAsync(999);

        // Assert
        Assert.False(result);
    }
}
