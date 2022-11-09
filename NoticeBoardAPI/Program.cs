using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoticeBoardAPI;
using NoticeBoardAPI.Entities;
using NoticeBoardAPI.Models;
using NoticeBoardAPI.Models.Validators;
using NoticeBoardAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddDbContext<NoticeBoardDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"));
});
builder.Services.AddScoped<NoticeBoardSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IAdvertService, AdvertService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUsedDtoValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<NoticeBoardSeeder>();
seeder.Seed();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
