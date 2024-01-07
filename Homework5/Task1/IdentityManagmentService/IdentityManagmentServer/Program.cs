using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(config =>
{
    config.UseInMemoryDatabase("Memory");
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "IdentityManagmentServer.Cookie";
    config.LoginPath = "/auth/login";
});

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddInMemoryApiResources(Configuration.GetApis())
    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
    .AddInMemoryClients(Configuration.GetClients())
    .AddDeveloperSigningCredential();
var app = builder.Build();


// Create user and add claim
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<IdentityUser>>();

    var user = new IdentityUser { UserName = "Sona" };
    userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
    userManager.AddClaimAsync(user, new Claim("role", "manager")).GetAwaiter().GetResult();
}
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
