using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Interface;

namespace API_GHSMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _service;

        public TestsController(ITestService testService)
        {
            _service = testService;
        }

        // GET: api/Tests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestDTO>>> GetTests()
        {
            var tests = await _service.GetAllAsync();
            return Ok(tests);
        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestDTO>> GetTest(int id)
        {
            var test = await _service.GetByIdAsync(id);
            if (test == null)
                return NotFound("Không tìm thấy xét nghiệm");

            return Ok(test);
        }

        // POST: api/Tests
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(dto);
            if (result > 0)
                return Ok(new { message = "Tạo xét nghiệm thành công", testId = result });

            return BadRequest("Tạo thất bại");
        }

        // PUT: api/Tests/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] TestDTO dto)
        {
            if (id != dto.TestId)
                return BadRequest("ID không khớp");

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Không tìm thấy xét nghiệm");

            var result = await _service.UpdateAsync(dto);
            if (result > 0)
                return Ok("Cập nhật thành công");

            return BadRequest("Cập nhật thất bại");
        }
    }
}
