using UsersService.API.DTOs;
using UsersService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UsersService.API.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var result = await userService.RegisterUserAsync(dto.Email, dto.Password, dto.Nickname);
        if (!result)
        {
            return BadRequest("User already exists");
        }

        return Ok("User registered successfully");
    }
}