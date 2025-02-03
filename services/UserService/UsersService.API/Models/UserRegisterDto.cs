namespace UsersService.API.DTOs;

public class UserRegisterDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Nickname { get; set; } = null!;
}