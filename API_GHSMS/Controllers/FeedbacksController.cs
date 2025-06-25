using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_GHSMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // GET: api/Feedbacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackDTO>>> GetFeedbacks()
        {
            var feedbacks = await _feedbackService.GetAllAsync();
            return Ok(feedbacks);
        }

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackDTO>> GetFeedback(int id)
        {
            var feedback = await _feedbackService.GetByIdAsync(id);
            if (feedback == null)
                return NotFound();

            return Ok(feedback);
        }

        // PUT: api/Feedbacks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FeedbackDTO feedback)
        {
            if (id != feedback.FeedbackId)
                return BadRequest("ID không khớp");

            var existing = await _feedbackService.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Không tìm thấy feedback");

            var result = await _feedbackService.UpdateAsync(feedback);
            if (result > 0)
                return Ok("Cập nhật feedback thành công");

            return BadRequest("Cập nhật thất bại");
        }

        // POST: api/Feedbacks
        [HttpPost]
        public async Task<IActionResult> PostFeedback([FromBody] FeedbackDTO feedback)
        {
            var result = await _feedbackService.CreateAsync(feedback);
            if (result > 0)
                return Ok(new { message = "Tạo feedback thành công", feedbackId = result });

            return BadRequest("Tạo feedback thất bại");
        }

        // DELETE: api/Feedbacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _feedbackService.GetByIdAsync(id);
            if (feedback == null)
                return NotFound("Không tìm thấy feedback");

            var result = await _feedbackService.DeleteAsync(id);
            if (result > 0)
                return NoContent();

            return BadRequest("Xoá feedback thất bại");
        }
    }
}
