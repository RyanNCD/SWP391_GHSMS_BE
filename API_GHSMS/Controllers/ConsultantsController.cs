using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Interface;


namespace API_GHSMS.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantController : ControllerBase
    {
        private readonly IConsultantService _consultantService;

        public ConsultantController(IConsultantService consultantService)
        {
            _consultantService = consultantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _consultantService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var consultant = await _consultantService.GetByIdAsync(id);
            if (consultant == null) return NotFound();
            return Ok(consultant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConsultantDTO dto)
        {
            var success = await _consultantService.CreateAsync(dto);
            if (!success) return BadRequest("Failed to create consultant");
            return Ok("Consultant created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ConsultantDTO dto)
        {
            var success = await _consultantService.UpdateAsync(id, dto);
            if (!success) return NotFound("Consultant not found or update failed");
            return Ok("Consultant updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _consultantService.DeleteAsync(id);
            if (!success) return NotFound("Consultant not found or delete failed");
            return Ok("Consultant deleted successfully");
        }
    }
