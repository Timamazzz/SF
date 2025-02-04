using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UsersService.Application.Interfaces;
using UsersService.Application.Services;
using UsersService.Core.Repositories;
using UsersService.Persistence;
using UsersService.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров в приложение
builder.Services.AddControllers();

// Настраиваем подключение к PostgreSQL, используя строку подключения из конфигурации
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрируем зависимости для репозиториев и сервисов
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Настраиваем Swagger для документирования API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SF API",
        Version = "v1",
        Description = "API SF",
    });

    // Подключаем XML-документацию для генерации комментариев в Swagger UI
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// В режиме разработки автоматически применяем миграции и включаем Swagger
if (app.Environment.IsDevelopment())
{
    ApplyMigrations(app);
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API v1"));
}

// Включаем маршрутизацию и авторизацию
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Запускаем приложение
app.Run();

void ApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    dbContext.Database.Migrate();
}