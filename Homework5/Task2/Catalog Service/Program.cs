using Microsoft.EntityFrameworkCore;
using Catalog.DataManager;
using Catalog.Interfaces.Repositories;
using Catalog.Interfaces.Services;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services
                    .AddTransient(typeof(IGenericRepository<>), typeof(EFGenericRepository<>))
                    .AddTransient<IProductService, ProductService>()
                    .AddTransient<ICategoryService, CategoryService>();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(connectionString));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

}