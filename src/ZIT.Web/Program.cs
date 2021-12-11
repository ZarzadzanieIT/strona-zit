using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Authentication.Cookies;
using ZIT.Infrastructure;
using ZIT.Infrastructure.Persistence;
using ZIT.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllersWithViews();
services.AddInfrastructure();
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.ReturnUrlParameter = "returnUrl";
        x.LoginPath = "/login";
        x.AccessDeniedPath = "/forbidden";
        x.Events.OnRedirectToLogin = CookieHelpers.HandleRedirectBasedOnUrl();
        x.Events.OnRedirectToAccessDenied = CookieHelpers.HandleRedirectBasedOnUrl();
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    await using var scope = app.Services.CreateAsyncScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedAsync();
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
