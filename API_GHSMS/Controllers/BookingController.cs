using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Repository.Util;
using Service.Interface;

namespace API_GHSMS.Controllers
{
    [ApiController]
    [Route("api/v1/booking")]
    public class BookingController(IBookingService _bookingService) : Controller
    {

        [HttpPost("booking")]
        [Authorize]
        public async Task<IActionResult> Booking([FromBody] BookingDTO request)
        {
            var userId = UserUtil.GetUserId(User);
            request.UserId = userId;

            var response = await _bookingService.Booking(request);
            if (response.Equals("208"))
            {
                return Conflict($"Your test schedule already exists, please check your history.");
            }

            return StatusCode(200, new { Message = "Booking success", BookingID = response });
        }

        [HttpGet("get-booking-by-user")]
        [Authorize]
        public async Task<IActionResult> GetUserBooking()
        {
            var userId = UserUtil.GetUserId(User);
            var response = await _bookingService.GetUserBooking(userId);
            return Ok(new { Message = "Get booking success", response });
        }

        [HttpPost("consulation-booking")]
        [Authorize]
        public async Task<IActionResult> ConsulationBooking([FromBody] ConsutantBooking request)
        {
            var userId = UserUtil.GetUserId(User);
            request.UserId = userId;
            var response = await _bookingService.ConsulationBooking(request);
            if (!response)
            {
                return StatusCode(500, new { Message = "Booking Fail" });
            }

            return StatusCode(200, "Booking Success");
        }

        [HttpGet("get-consulation-booking")]
        [Authorize]
        public async Task<IActionResult> GetConsulationBookings()
        {
            var userId = UserUtil.GetUserId(User);
            var reponse = await _bookingService.GetConculationBookings(userId);
            return StatusCode(200, reponse);
        }
    }
}
