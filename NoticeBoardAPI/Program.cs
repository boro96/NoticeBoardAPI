using Microsoft.EntityFrameworkCore;
using NoticeBoardAPI;
using NoticeBoardAPI.Entities;
using NoticeBoardAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<NoticeBoardDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"));
});
builder.Services.AddScoped<NoticeBoardSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IAdvertService, AdvertService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<NoticeBoardSeeder>();
seeder.Seed();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
