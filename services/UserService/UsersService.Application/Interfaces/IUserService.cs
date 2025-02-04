namespace UsersService.Application.Interfaces;

// Интерфейс сервиса для работы с пользователями
public interface IUserService
{
    // Регистрирует нового пользователя
    Task<bool> RegisterUserAsync(string email, string password, string nickname);
}