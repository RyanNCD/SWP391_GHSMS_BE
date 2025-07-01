using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Interface;

namespace API_GHSMS.Controllers
{
    [ApiController]
    [Route("api/v1/test-result")]
    public class TestResultController(ITestResultService _service) : Controller
    {
        [HttpPost("create-test-result")]
        public async Task<IActionResult> CreateTestResult([FromBody] CreateTestResult request)
        {
            var response = await _service.CreateTestResult(request);
            if (response == true)
            {
                return Ok(new { Message = "Create Test Result Success" });

            }

            return BadRequest("Create test result fail");
        }

        [HttpPatch("edit-test-result/{resultId}")]
        public async Task<IActionResult> EditTestResult(Guid resultId, [FromBody] CreateTestResult request)
        {
            var response = await _service.UpdateTestResult(resultId, request);
            if (response == null)
            {
                return NotFound("Test result not found");
            }

            return Ok(new { Message = "Update Test Result Success" });
        }

        [HttpGet("get-test-result-by-user-booking")]
        public async Task<IActionResult> GetTestResultByUserBooking([FromQuery] Guid bookingId)
        {
            var response = await _service.GetTestResultByUserBooking(bookingId);
            return Ok(new { Message = "Test result success", Data = response });
        }

    }
}
