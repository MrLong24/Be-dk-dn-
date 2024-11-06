using EventFlowerExchange.Repositories.Interfaces;
using EventFlowerExchange.services.DTO;
using EventFlowerExchange.services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowerExchange.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Đăng ký người dùng
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                var user = await _userService.RegisterAsync(userDto);
                return Ok(new { message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Đăng nhập người dùng
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) // Cập nhật tham số thành LoginDto
        {
            if (loginDto == null)
            {
                return BadRequest(new { message = "LoginDto is required." });
            }

            try
            {
                var token = await _userService.LoginAsync(loginDto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
