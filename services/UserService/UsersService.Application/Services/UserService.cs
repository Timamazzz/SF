namespace UsersService.Application.Services;

using UsersService.Application.Interfaces;
using UsersService.Core.Models;
using UsersService.Core.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// Реализация сервиса пользователей
public class UserService(IUserRepository userRepository) : IUserService
{
    // Метод для регистрации нового пользователя
    // Возвращает true, если регистрация успешна, иначе false (например, если email уже занят)
    public async Task<bool> RegisterUserAsync(string email, string password, string nickname)
    {
        // Проверяем, существует ли уже пользователь с таким email
        if (await userRepository.GetByEmailAsync(email) != null)
        {
            return false; // Если да, регистрация не выполняется
        }

        // Создаем нового пользователя с хешированным паролем
        var user = new User
        {
            Email = email,
            PasswordHash = HashPassword(password),
            Nickname = nickname
        };

        // Добавляем пользователя в хранилище
        await userRepository.AddAsync(user);
        return true;
    }

    // Метод для хеширования пароля с использованием SHA-256
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }
}