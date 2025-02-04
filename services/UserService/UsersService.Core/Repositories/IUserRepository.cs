namespace UsersService.Core.Repositories;

using UsersService.Core.Models;

// Интерфейс репозитория для работы с пользователями
public interface IUserRepository
{
    // Получает пользователя по email, если он существует
    Task<User?> GetByEmailAsync(string email);

    // Добавляет нового пользователя в хранилище
    Task AddAsync(User user);
}