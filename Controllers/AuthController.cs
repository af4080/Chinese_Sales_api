using Microsoft.AspNetCore.Mvc;
using projectApiAngular.Services;
using static projectApiAngular.DTO.UserDto;
using static projectApiAngular.DTO.UserDto.ReadUserDto;

namespace projectApiAngular.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        //login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _userService.LoginUser(loginDto.Email, loginDto.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }

        //register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto userDto)
        {
            try
            {

                var createdUser = await _userService.RegisterUser(userDto);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
