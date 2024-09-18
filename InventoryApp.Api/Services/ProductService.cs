using AutoMapper;
using InventoryApp.Api.Configuration;
using InventoryApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Api.Services;
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(ProductDto product);
    Task<bool> UpdateAsync(ProductDto product);
    Task<bool> DeleteAsync(int id);
}



public class ProductService(ApplicationDbContext context, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await context.Products
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .ToListAsync();

        return mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await context.Products
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.ProductID == id);

        if (product == null) return null;

        return mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateAsync(ProductDto productDto)
    {
        var product = mapper.Map<Product>(productDto);

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return mapper.Map<ProductDto>(product);
    }

    public async Task<bool> UpdateAsync(ProductDto productDto)
    {
        var product = await context.Products
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.ProductID == productDto.ProductID);

        if (product == null) return false;

        mapper.Map(productDto, product);

        context.Products.Update(product);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null) return false;

        context.Products.Remove(product);
        return await context.SaveChangesAsync() > 0;
    }
}

