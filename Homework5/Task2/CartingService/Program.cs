using AutoMapper;
using CartingService.BLL.Mapping;
using CartingService.BLL.Services;
using CartingService.DAL.Data;
using CartingService.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICartRepository, CartRepository>();
builder.Services.AddSingleton<ICartService, CartService>();
IMapper mapper = MapperConfig.InitializeAutomapper();
builder.Services.AddSingleton(mapper);
var dbColName = builder.Configuration.GetValue<string>("CollectionName");
var dbPath = Path.Combine(Environment.CurrentDirectory, dbColName);
builder.Services.AddSingleton(new ApplicationDBContext(dbPath));
builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer");
    //.AddJwtBearer("Bearer", options =>
    //{
    //    options.Authority = "https://localhost:5001";
    //    options.RequireHttpsMetadata = false;
    //    options.Audience = "carting";
    //});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "Carts V1");
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();