using  Microsoft.EntityFrameworkCore;
using Basket.DataManager;
using Basket.Interfaces.Repositories;
using Basket.DataManager.Repositories;
using Basket.Interfaces.Services;

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
             .AddTransient<IBasketService, BasketService>();

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