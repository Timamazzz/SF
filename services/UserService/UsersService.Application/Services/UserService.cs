namespace UsersService.Application.Services;

using UsersService.Application.Interfaces;
using UsersService.Core.Models;
using UsersService.Core.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<bool> RegisterUserAsync(string email, string password, string nickname)
    {
        if (await userRepository.GetByEmailAsync(email) != null)
        {
            return false;
        }

        var user = new User
        {
            Email = email,
            PasswordHash = HashPassword(password),
            Nickname = nickname
        };

        await userRepository.AddAsync(user);
        return true;
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }
}