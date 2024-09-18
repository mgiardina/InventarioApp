using AutoMapper;
using InventoryApp.Api.Models;

namespace InventoryApp.Api.Configuration;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)));

        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.ProductCategories, opt => opt.MapFrom(src => src.Categories.Select(c => new ProductCategory { CategoryID = c.CategoryID })));

        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}
