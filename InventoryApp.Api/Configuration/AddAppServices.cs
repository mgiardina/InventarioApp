using InventoryApp.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Api.Configuration;

public static class ServiceExtensions
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("InventoryDB"));
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
    }
}
