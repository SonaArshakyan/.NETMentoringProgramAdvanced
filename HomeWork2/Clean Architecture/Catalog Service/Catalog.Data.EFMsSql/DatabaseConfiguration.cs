using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Data.EFMsSql;

public static class DatabaseConfiguration
{
    public static void AddInfrastructureWithEFMsSQL(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(
           options => options.UseSqlServer(connectionString));
    }
}
