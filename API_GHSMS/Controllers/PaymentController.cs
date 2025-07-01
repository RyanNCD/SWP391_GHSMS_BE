using EmailService.DTO;
using EmailService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayOSService.DTO;
using PayOSService.Services;
using Repository.Models;
using Repository.Util;

namespace API_GHSMS.Controllers
{
    [ApiController]
    [Route("api/v1/payment")]
    public class PaymentController(IPayOSService _service, SWP391GHSMContext _context, IEmailSender _sender) : Controller
    {
        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] Guid bookingId)
        {
            var getBooking = await _context.TestBookings.Include(x => x.Test).FirstOrDefaultAsync(x => x.TestBookingId == bookingId);

            var checkTransaction = await _context.Transactions.FirstOrDefaultAsync(x => x.BookingId == bookingId);
            if (checkTransaction != null)
            {
                return Ok(checkTransaction.Url);
            }

            if (getBooking == null)
            {
                return NotFound();
            }

            var paymentUrl = new CreatePaymentDTO
            {
                Content = "Payment",
                OrderCode = UserUtil.GenerateOrderCode(),
                RequiredAmount = (int)getBooking.Test.Price
            };

            string url = await _service.CreatePaymentAsync(paymentUrl);

            var createTransaction = new Transaction
            {
                Id = paymentUrl.OrderCode,
                BookingId = getBooking.TestBookingId,
                CreateAt = DateTime.Now,
                Url = url
            };

            await _context.Transactions.AddAsync(createTransaction);
            await _context.SaveChangesAsync();
            return Ok(url);
        }


        [HttpPost("return-payment")]
        public async Task<IActionResult> PaymentReturn([FromQuery] string code, [FromQuery] long orderId, [FromQuery] string status, [FromQuery] bool cancel)
        {
            var getBooking = await _context.Transactions
                                               .Include(x => x.Booking)
                                               .ThenInclude(x => x.Test)
                                               .FirstOrDefaultAsync(x => x.Id == orderId);
            if (code == "00" && status == "PAID")
            {

                if (getBooking == null) { return NotFound(); }

                getBooking.Booking.Status = "PAID";
                _context.Transactions.Update(getBooking);
                await _context.SaveChangesAsync();
                var rawHtml = CommonUtil.HTMLLoading("Notification.html");

                var finalHtml = rawHtml
                                .Replace("[ngay_test]", getBooking.Booking.ScheduledDate.ToString())
                                .Replace("[gia]", getBooking.Booking.Test.Price.ToString())
                                .Replace("[trang_thai]", getBooking.Booking.Status);

                var getUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == getBooking.Booking.UserId);

                var emailRequest = new EmailRequest<string>
                {
                    To = getUser.Email,
                    Subject = "[Đặt Lịch Xét Nghiệm Thành Công]",
                    Body = finalHtml
                };

                await _sender.SendEmailAsync(emailRequest);
                return Ok("Confirm Payment Success");


            }

            else
            {
                var getTest = await _context.Tests.FirstOrDefaultAsync(x => x.TestId == getBooking.Booking.TestId);
                getTest.IsBooked = false;
                getBooking.Booking.Status = "Payment Cancelled";
                _context.Tests.Update(getTest);
                _context.Transactions.Update(getBooking);
                await _context.SaveChangesAsync();

                var rawHtml = CommonUtil.HTMLLoading("Notification.html");
                var finalHtml = rawHtml
                              .Replace("[ngay_test]", getBooking.Booking.ScheduledDate.ToString())
                              .Replace("[gia]", getBooking.Booking.Test.Price.ToString())
                              .Replace("[trang_thai]", getBooking.Booking.Status);

                var getUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == getBooking.Booking.UserId);

                var emailRequest = new EmailRequest<string>
                {
                    To = getUser.Email,
                    Subject = "[Đặt Lịch Xét Nghiệm Thành Công]",
                    Body = finalHtml
                };

                await _sender.SendEmailAsync(emailRequest);

                return Ok("Payment Cancelled");
            }
        }
    }
}
