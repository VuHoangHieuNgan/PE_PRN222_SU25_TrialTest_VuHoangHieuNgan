using LionPetManagement_VuHoangHieuNgan.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Repositories.Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<LionAccountService>();
builder.Services.AddScoped<LionProfileService>();
builder.Services.AddScoped<LionTypeService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Authentication/AccessDenied";
        options.LogoutPath = "/Authentication/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

//app.MapRazorPages().RequireAuthorization();

app.MapHub<LionProfileHub>("/LionProfileHub");

app.Run();
