namespace UsersService.Application.Services;

using UsersService.Core.Models;
using UsersService.Core.Repositories;
using System.Security.Cryptography;
using System.Text;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> RegisterUserAsync(string email, string password, string nickname)
    {
        if (await _userRepository.GetByEmailAsync(email) != null)
        {
            return false; // Пользователь уже существует
        }

        var user = new User
        {
            Email = email,
            PasswordHash = HashPassword(password),
            Nickname = nickname
        };

        await _userRepository.AddAsync(user);
        return true;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }
}