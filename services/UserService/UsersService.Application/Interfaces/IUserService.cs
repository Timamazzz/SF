namespace UsersService.Application.Interfaces;

public interface IUserService
{
    Task<bool> RegisterUserAsync(string email, string password, string nickname);
}