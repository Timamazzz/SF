using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UsersService.Application.Interfaces;
using UsersService.Application.Services;
using UsersService.Core.Repositories;
using UsersService.Persistence;
using UsersService.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров
builder.Services.AddControllers();

// Настроим подключение к PostgreSQL с использованием строки подключения из конфигурации
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Подключаем репозитории и сервисы
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Настройка Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SF API",
        Version = "v1",
        Description = "API SF",
    });
});

var app = builder.Build();

// Применяем миграции автоматически в Development-режиме
if (app.Environment.IsDevelopment())
{
    ApplyMigrations(app);
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API v1"));
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Автоматическое применение миграций
void ApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    dbContext.Database.Migrate();
}