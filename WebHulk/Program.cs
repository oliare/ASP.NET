using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebHulk.Constants;
using WebHulk.Data;
using WebHulk.Data.Entities.Identity;
using WebHulk.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HulkDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AppMapProfile));
builder.Services.AddScoped<DataSeeder>();

// Identity options
builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = true;

    //options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<HulkDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

string dirSave = Path.Combine(Directory.GetCurrentDirectory(), "images");
if (!Directory.Exists(dirSave))
    Directory.CreateDirectory(dirSave);

app.UseRouting();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dirSave),
    RequestPath = "/images"
});

//
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Main}/{action=Index}/{id?}");

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "admin_area",
        areaName: "Admin",
        pattern: "admin/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Main}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
      name: "access_denied",
      pattern: "{controller=Account}/{action=AccessDenied}/{id?}");
        
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HulkDbContext>();
    //dbContext.Database.EnsureDeleted();
    dbContext.Database.Migrate();
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    seeder.SeedProducts();
    await seeder.SeedRolesAndUsers();
}

app.Run();