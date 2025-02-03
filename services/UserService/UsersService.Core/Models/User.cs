namespace UsersService.Core.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public List<Guid> Friends { get; set; } = new();
}