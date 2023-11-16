using AutoMapper;
using Application.Models;
using Domain;

namespace Application.Common;

public class MapperConfig
{
    public static Mapper InitializeAutomapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CategoryDTO, Category>();
            cfg.CreateMap<Category, CategoryDTO>();
            cfg.CreateMap<ProductDTO, Product>();
            cfg.CreateMap<Product, ProductDTO>();
        });

        var mapper = new Mapper(config);
        return mapper;
    }
}
