using UsersService.API.Models;
using UsersService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UsersService.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="dto">Данные для регистрации пользователя</param>
        /// <returns>Результат регистрации пользователя</returns>
        /// <response code="200">Пользователь успешно зарегистрирован</response>
        /// <response code="400">Пользователь с таким email уже существует</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            // Регистрируем пользователя с данными из DTO
            var result = await userService.RegisterUserAsync(dto.Email, dto.Password, dto.Nickname);

            // Если регистрация не успешна (пользователь уже существует)
            if (!result)
            {
                return BadRequest("User already exists");
            }

            // Успешная регистрация
            return Ok("User registered successfully");
        }
    }
}