using  Microsoft.EntityFrameworkCore;
using Basket.DataManager;
using Basket.Interfaces.Repositories;
using Basket.DataManager.Repositories;
using Basket.Interfaces.Services;
using AzureServiceBusMessageBroker;
using AzureServiceBusMessageBroker.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<IListnerService, ListnerService>();
        builder.Services
             .AddTransient(typeof(IGenericRepository<>), typeof(EFGenericRepository<>))
             .AddTransient<IBasketService, BasketService>();
        ConfigureServiceBusClient(builder);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(connectionString));

        builder.Services.AddHostedService<BasketItemUpdater>();

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

    private static void ConfigureServiceBusClient(WebApplicationBuilder builder)
    {
        string serviceBusConnectionString = builder.Configuration.GetSection("AzureServiceBus:ServiceBusConnectionString").Value;
        string topicName = builder.Configuration.GetSection("AzureServiceBus:TopicName").Value;
        string subscriptionName = builder.Configuration.GetSection("AzureServiceBus:SubscriptionName").Value;

        builder.Services.AddSingleton<AzureServiceBusClient>(p =>
                            new AzureServiceBusClient(serviceBusConnectionString, topicName, subscriptionName));
    }
}