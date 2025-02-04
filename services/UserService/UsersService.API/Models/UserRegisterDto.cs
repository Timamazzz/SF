namespace UsersService.API.Models;
/// <summary>
/// DTO для регистрации пользователя
/// </summary>
public class UserRegisterDto
{
    /// <summary>
    /// Электронная почта пользователя
    /// </summary>
    /// <example>example@example.com</example>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Пароль пользователя (передается в открытом виде, должен быть захеширован перед сохранением)
    /// </summary>
    /// <example>password123</example>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Никнейм пользователя
    /// </summary>
    /// <example>nickname123</example>
    public string Nickname { get; set; } = null!;
}
