using Controller.Services;
using DataAccess.Repositories;
using DataAccess.RepositoriesImpl;
using DataAccess.Repository.Impl;
using DataAccess.Service.Impl;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using UserInterface.AuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddScoped<TruckerAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, TruckerAuthenticationProvider>();
builder.Services.AddScoped<ILoginService, TruckerAuthenticationProvider>();
builder.Services.AddScoped<IShipmentRepository,ShipmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy => policy.RequireAuthenticatedUser());
    //options.AddPolicy("SuperadminPolicy", policy => policy.RequireRole("Superadmin"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();