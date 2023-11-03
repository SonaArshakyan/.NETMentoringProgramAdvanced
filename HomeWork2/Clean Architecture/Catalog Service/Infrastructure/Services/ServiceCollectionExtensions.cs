using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Catalog.Data.EFMsSql;

namespace Infrastructure.Services;

public static class ServiceCollectionExtensions
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

         services.AddInfrastructureWithEFMsSQL(connectionString);
     
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        IMapper mapper = Application.Common.MapperConfig.InitializeAutomapper();

        services
            .AddTransient(typeof(IGenericRepository<>), typeof(EFGenericRepository<>))
            .AddTransient<IProductService, ProductService>()
            .AddTransient<ICategoryService, CategoryService>()            
            .AddSingleton(mapper);
    }
}
