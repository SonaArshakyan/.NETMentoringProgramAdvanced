using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces.Repositories;
using Infrastructure.Interfaces.Repositories;
using Application.Interfaces.Services;


namespace Infrastructure.Services;

public static class IServiceCollectionExtensions
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

         services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(connectionString));

    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddTransient<IProductService, ProductService>()
            .AddTransient<ICategoryService, CategoryService>();
    }
}
