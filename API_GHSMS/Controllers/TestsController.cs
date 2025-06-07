using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Implement;
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
        public async Task<ActionResult<Test>> GetTest(int id)
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
        public async Task<ActionResult> Update(int id, [FromBody] Test test)
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
        public async Task<ActionResult> Create([FromBody] Test test)
        {
            var result = await _service.CreateAsync(test);
            if (result > 0)
                return Ok(new { message = "Test created", Testid = test.TestId });
            return BadRequest("Failed to create test");
        }

        
    }
}
