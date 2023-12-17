using IdentityManagementSystem;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentityServer()
        .AddDeveloperSigningCredential()
        .AddInMemoryIdentityResources(Config.IdentityResourcesWithRoles)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryClients(Config.Clients);

var app = builder.Build();
app.UseIdentityServer();

app.Run();

