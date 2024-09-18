using InventoryApp.Api.Models;
using InventoryApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetProducts()
    {
        var products = await productService.GetAllAsync();
        return Results.Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetProduct(int id)
    {
        var product = await productService.GetByIdAsync(id);
        return product != null ? Results.Ok(product) : Results.NotFound();
    }

    [HttpPost]
    public async Task<IResult> CreateProduct(ProductDto productDto)
    {
        var createdProduct = await productService.CreateAsync(productDto);
        return Results.Created($"/api/products/{createdProduct.ProductID}", createdProduct);
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateProduct(int id, ProductDto productDto)
    {
        if (id != productDto.ProductID) return Results.BadRequest();

        var success = await productService.UpdateAsync(productDto);
        return success ? Results.NoContent() : Results.NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteProduct(int id)
    {
        var success = await productService.DeleteAsync(id);
        return success ? Results.NoContent() : Results.NotFound();
    }
}

