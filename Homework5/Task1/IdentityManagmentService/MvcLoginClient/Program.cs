
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(config =>
{
    config.DefaultScheme = "Cookie";
    config.DefaultChallengeScheme = "oidc";

})
                .AddCookie("Cookie")
                .AddOpenIdConnect("oidc" , options =>
                {
                    options.ClientId = "client_id_mvc";
                    options.ClientSecret = "client_secret_mvc";
                    options.SaveTokens = true;
                    options.Authority = "https://localhost:7110/";
                    options.ResponseType = "code";
                    options.Scope.Add("userRoles.Scope");
                    options.Scope.Add("ManagersAPI");
                    options.Scope.Add("BuyersAPI");
                    options.Scope.Add("offline_access");
                    options.UsePkce = true;
                });

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
