using IdentityManagementSystem;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentityServer()
        .AddDeveloperSigningCredential()
        .AddInMemoryIdentityResources(Config.IdentityResourcesWithRoles)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryClients(Config.Clients);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.RequireHttpsMetadata = false;
        //options.Audience = "catalog";
    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
//    options.AddPolicy("BuyerPolicy", policy => policy.RequireRole("Buyer"));
//});

builder.Services.AddControllers();

var app = builder.Build();
app.UseIdentityServer();

app.UseHttpsRedirection();
app.Run();

