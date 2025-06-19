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
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // GET: api/Feedbacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            return await _feedbackService.GetAllAsync();
        }

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
            var feedback = await _feedbackService.GetByIdAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }

        // PUT: api/Feedbacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
      
         public async Task<ActionResult> Update(int id, [FromBody] Feedback feedback)
        {
            if (id != feedback.FeedbackId)
                return BadRequest("ID mismatch");
            var existing = await _feedbackService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();


            var result = await _feedbackService.UpdateAsync(feedback);
            if (result > 0)
                return Ok("Feedback updated");
            return BadRequest("Update failed");
        }

        // POST: api/Feedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostFeedback(Feedback feedback)
        {
            var result = await _feedbackService.CreateAsync(feedback);
            if (result > 0)
                return Ok(new { message = "Feedback created", feedbackId = feedback.FeedbackId });
            return BadRequest("Failed to create feedback");
        }

        // DELETE: api/Feedbacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _feedbackService.GetByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

           await _feedbackService.DeleteAsync(id);
            

            return NoContent();
        }

       
    }
}
