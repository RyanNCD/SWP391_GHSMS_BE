using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Interface;


namespace API_GHSMS.Controllers
{
    [ApiController]
    [Route("api/v1/consultants")]
    public class ConsultantsController(IConsultantsService _consultantService) : Controller
    {
        [HttpPost("create-consultant")]
        public async Task<IActionResult> CreateConsultant([FromForm] CreateConsultantsDTO request)
        {
            try
            {
                var response = await _consultantService.CreateConsultants(request);
                if (response == false)
                {
                    return Conflict(new { Message = "Email Already Exist" });
                }

                return StatusCode(200, new { Message = "Create success" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-Consultants")]
        public async Task<IActionResult> GetConsultants()
        {
            var response = await _consultantService.GetConsultants();
            return StatusCode(200, new { Message = "Success", Data = response });
        }

        [HttpGet("get-detail")]
        public async Task<IActionResult> GetConsultantsDetail([FromQuery] Guid consultantsId)
        {
            var response = await _consultantService.GetConsultantDetail(consultantsId);
            if (response == null)
            {
                return NotFound(new { Message = "Data not found" });
            }

            return StatusCode(200, new { Message = response });
        }

        [HttpDelete("delete-consultants")]
        public async Task<IActionResult> DeleteConsultants([FromQuery] Guid consultantsId)
        {
            var response = await _consultantService.DeleteConsultants(consultantsId);
            if (response == false)
            {
                return NotFound("Consultants not found");
            }

            return StatusCode(200, new { Message = "Delete success" });
        }

        [HttpPatch("edit-consultants/{consultantsId}")]
        public async Task<IActionResult> EditConsultants(Guid consultantsId, [FromBody] CreateConsultantsDTO request)
        {
            var response = await _consultantService.EditConsultantDetail(consultantsId, request);
            if (response == false)
            {
                return StatusCode(404, new { Message = "Data not found" });
            }

            return StatusCode(200, new { Message = "Edit Success" });
        }
    }
}