using Microsoft.AspNetCore.Authentication.Cookies;
using Southwest_Airlines.Services;
using Southwest_Airlines.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DBCustomer>();
builder.Services.AddScoped<DBEmployee>();
builder.Services.AddScoped<SkipLoginValidationFilter>();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Home/Login";
//        options.LogoutPath = "/Home/Logout";
//        options.AccessDeniedPath = "/Home/AccessDenied";
//    });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true; // Ensure the cookie is not accessible via JavaScript
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Optional: Set a timeout for the session
        options.SlidingExpiration = true; // Optional: Extend the session on activity
        options.Cookie.IsEssential = true; // Ensure the cookie is essential for GDPR compliance
        options.Cookie.SameSite = SameSiteMode.Strict; // Optional: Restrict cross-site usage
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use secure cookies (HTTPS only)
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=info}/{id?}");

app.Run();
