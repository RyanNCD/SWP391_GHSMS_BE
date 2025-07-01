using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Repository.Models;
using Repository.Util;
using Service.Interface;

namespace API_GHSMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _service;

        public TestsController(ITestService testService)
        {
            _service = testService;
        }

        // GET: api/Tests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(Guid id)
        {
            var test = await _service.GetByIdAsync(id);

            if (test == null)
            {
                return NotFound();
            }

            return test;
        }

        // PUT: api/Tests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] Test test)
        {
            if (id != test.TestId)
                return BadRequest("ID mismatch");

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            var result = await _service.UpdateAsync(test);
            if (result > 0)
                return Ok("User updated");
            return BadRequest("Update failed");
        }

        // POST: api/Tests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Consutant")]
        public async Task<ActionResult> Create([FromBody] TestDTO test)
        {
            var userId = UserUtil.GetUserId(User);
            test.consultantId = userId;
            var result = await _service.AddTest(test);
            if (result == true)
                return Ok(new { message = "Test created" });
            return BadRequest("Failed to create test");
        }


        [HttpGet("get-test-by-consutant")]
        public async Task<IActionResult> GetTestByConsutant([FromQuery] Guid ConsutantId)
        {
            var response = await _service.GetTestByConsutant(ConsutantId);
            return Ok(response);
        }

        [HttpGet("get-test-detail")]
        public async Task<IActionResult> GetTestDetail([FromQuery] Guid TestId)
        {
            var response = await _service.GetTestDetailAsync(TestId);
            if (response == null)
            {
                return NotFound("Test not found");
            }

            return Ok(response);
        }

    }
}
