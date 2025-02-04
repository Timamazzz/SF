namespace UsersService.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using UsersService.Core.Models;
using UsersService.Core.Repositories;

// Реализация репозитория пользователей, использующего Entity Framework Core
public class UserRepository(UserDbContext context) : IUserRepository
{
    // Поиск пользователя по email в базе данных
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    // Добавление нового пользователя в базу данных
    public async Task AddAsync(User user)
    {
        context.Users.Add(user); // Добавляем пользователя в контекст
        await context.SaveChangesAsync(); // Сохраняем изменения в базе данных
    }
}