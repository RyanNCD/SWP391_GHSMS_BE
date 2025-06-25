using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
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

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<List<UserProfileDTO>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDTO>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound("Không tìm thấy người dùng");

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO dto)
        {
            var result = await _userService.CreateAsync(dto);
            if (result > 0)
                return CreatedAtAction(nameof(GetById), new { id = result }, new { message = "Tạo người dùng thành công", userId = result });

            return BadRequest("Tạo người dùng thất bại");
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserProfileDTO dto)
        {
            if (id != dto.UserId)
                return BadRequest("ID không khớp");

            var existing = await _userService.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Không tìm thấy người dùng");

            var result = await _userService.UpdateAsync(dto);
            if (result >= 0)
                return Ok("Cập nhật người dùng thành công");

            return BadRequest("Cập nhật thất bại");
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteByIdAsync(id);
            if (result)
                return Ok("Xóa người dùng thành công");

            return NotFound("Không tìm thấy người dùng");
        }

        // GET: api/User/user-profile/5
        [HttpGet("user-profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _userService.GetProfileAsync(id);
            if (profile == null)
                return NotFound("Không tìm thấy hồ sơ");

            return Ok(profile);
        }

        // PUT: api/User/user-profile
        [HttpPut("user-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDTO dto)
        {
            var success = await _userService.UpdateProfileAsync(dto);
            if (!success)
                return NotFound("Không tìm thấy người dùng");

            return Ok("Cập nhật hồ sơ thành công");
        }
    }
}
