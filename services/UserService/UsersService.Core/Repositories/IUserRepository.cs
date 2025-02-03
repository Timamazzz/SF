namespace UsersService.Core.Repositories;

using UsersService.Core.Models;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}