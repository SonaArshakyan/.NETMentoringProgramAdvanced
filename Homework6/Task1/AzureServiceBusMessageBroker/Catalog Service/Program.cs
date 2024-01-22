using Microsoft.EntityFrameworkCore;
using Catalog.DataManager;
using Catalog.Interfaces.Repositories;
using Catalog.Interfaces.Services;
using AzureServiceBusMessageBroker.Services;
using AzureServiceBusMessageBroker;

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
                    .AddTransient<ICategoryService, CategoryService>()
                    .AddTransient<IMessagePublisher, ProductMessagePublisher>()
                    .AddTransient<IPublisherService, PublisherService>();

        ConfigureServiceBusClient(builder);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(connectionString));
        builder.Services.AddApplicationInsightsTelemetry(builder.Configuration.GetSection("ApplicationInsights:InstrumentationKey").Value);

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