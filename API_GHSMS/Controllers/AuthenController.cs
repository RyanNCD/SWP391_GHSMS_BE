using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Interface;

namespace API_GHSMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _authenService;

        public AuthenController(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authenService.LoginWithToken(loginDto.Email, loginDto.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid credentials." });

            return Ok(new { token });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            var user = await _authenService.RegisterAsync(model.FullName, model.Email, model.Password, model.RoleId, model.PhoneNumber, model.Gender);
            return Ok(user);
        }
    }
}
