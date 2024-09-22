using ApiStore.Data;
using ApiStore.Data.Entities;
using ApiStore.Interfaces;
using ApiStore.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApiStoreDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddAutoMapper(typeof(AppMapProfile));
builder.Services.AddScoped<IImageTool, ImageHulk>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
    opt.AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());

app.UseAuthorization();
app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["ImagesDir"])),
    RequestPath = "/images"
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApiStoreDbContext>();
    //dbContext.Database.EnsureDeleted();
    dbContext.Database.Migrate();

    if (dbContext.Categories.Count() == 0)
    {
        var cat = new CategoryEntity()
        {
            Name = "Dogs",
            Description = "Dogs of different types",
            Image = "dog.jpg"
        };
        dbContext.Categories.Add(cat);
        dbContext.SaveChanges();
    }
}

app.Run();
