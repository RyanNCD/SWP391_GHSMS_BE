using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Repository.Models;
using Service.Interface;

namespace API_GHSMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
       

        public UserController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] User user)
        {
            var result = await _userService.CreateAsync(user);
            if (result > 0)
                return Ok(new { message = "User created", userId = user.UserId });
            return BadRequest("Failed to create user");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] User user)
        {
            if (id != user.UserId)
                return BadRequest("ID mismatch");

            var existing = await _userService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            var result = await _userService.UpdateAsync(user);
            if (result > 0)
                return Ok("User updated");
            return BadRequest("Update failed");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _userService.DeleteByIdAsync(id);
            if (result)
            {
                return Ok("User deleted");
            }
            else
            {
                return NotFound("User Not Found");
            }
        }
        [HttpGet("user-profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _userService.GetProfileAsync(id);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPut("user-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDTO dto)
        {
            var success = await _userService.UpdateProfileAsync(dto);
            if (!success)
                return NotFound("User not found");

            return Ok("Profile updated");
        }

    }
}
