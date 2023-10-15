using AutoMapper;
using CartingService.BLL.Model;
using CartingService.DAL.Entities;

namespace CartingService.BLL.Mapping;

public  class MapperConfig
{
    public static Mapper InitializeAutomapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CartModel, Cart>();
            cfg.CreateMap<Cart, CartModel>();
        });

        var mapper = new Mapper(config);
        return mapper;
    }
}