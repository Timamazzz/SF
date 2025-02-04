namespace UsersService.Core.Models;

// Модель пользователя, содержащая основные данные
public class User
{
    // Уникальный идентификатор пользователя, генерируется при создании
    public Guid Id { get; set; } = Guid.NewGuid();

    // Электронная почта пользователя
    public string Email { get; set; } = null!;

    // Хешированный пароль пользователя
    public string PasswordHash { get; set; } = null!;

    // Уникальный никнейм пользователя
    public string Nickname { get; set; } = null!;

    // Список идентификаторов друзей
    public List<Guid> Friends { get; set; } = new();
}