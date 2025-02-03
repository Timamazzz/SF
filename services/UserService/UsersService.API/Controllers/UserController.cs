namespace UsersService.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using UsersService.API.DTOs;
using UsersService.Application.Services;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var result = await _userService.RegisterUserAsync(dto.Email, dto.Password, dto.Nickname);
        if (!result)
        {
            return BadRequest("User already exists");
        }

        return Ok("User registered successfully");
    }
}