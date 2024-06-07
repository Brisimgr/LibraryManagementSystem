using LibraryApp.Components;
using LibraryApp.Models;
using LibraryApp.Data;
using LibraryApp.Services;
using Microsoft.EntityFrameworkCore;
using LibraryApp;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options => {
            options.AccessDeniedPath = "/access-denied";
            options.Cookie.Name = "auth_token";
            options.LoginPath = "/login";
            options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
       });  
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();  

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBorrowedService, BorrowedService>();
builder.Services.AddScoped<IGenresService, GenresService>();
builder.Services.AddScoped<IUserService, UserService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<LibraryDbContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
