using EmailService.DTO;
using EmailService.Interface;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.DTO;
using Repository.Models;
using Repository.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BookingRepository(SWP391GHSMContext _context, IEmailSender _sender) : GenericRepository<TestBooking>
    {
        public async Task<string> Booking(BookingDTO request)
        {
            try
            {
                var checkBookingDate = await _context.TestBookings
                                     .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.TestId == request.TestId);
                if (checkBookingDate != null)
                {
                    return "208";
                }

                var getTest = await _context.Tests.FirstOrDefaultAsync(x => x.TestId == request.TestId);
                getTest.isBooked = true;

                _context.Tests.Update(getTest);
                var createBooking = new TestBooking
                {
                    TestBookingId = Guid.NewGuid(),
                    UserId = request.UserId,
                    TestId = request.TestId,
                    ScheduledDate = getTest.Date,
                    Status = "Wait For Payment"
                };

                var rawHtml = CommonUtil.HTMLLoading("Notification.html");

                var finalHtml = rawHtml
                                .Replace("[ngay_test]", createBooking.ScheduledDate.ToString())
                                .Replace("[gia]", getTest.Price.ToString())
                                .Replace("[trang_thai]", createBooking.Status);

                var getUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

                var emailRequest = new EmailRequest<string>
                {
                    To = getUser.Email,
                    Subject = "[Đặt Lịch Xét Nghiệm Thành Công]",
                    Body = finalHtml
                };

                await _sender.SendEmailAsync(emailRequest);

                await _context.TestBookings.AddAsync(createBooking);
                await _context.SaveChangesAsync();
                return createBooking.TestBookingId.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        public async Task<List<GetBookingByUserResponse>> GetBookingByUser(Guid userId)
        {
            var getBookings = await _context.TestBookings.Where(x => x.UserId == userId).ToListAsync();

            var mapItem = getBookings.Select(x => new GetBookingByUserResponse
            {
                TestBookingId = x.TestBookingId,
                scheduledDate = x.ScheduledDate,
                Status = x.Status
            }).ToList();

            return mapItem;
        }

        public async Task<bool> BookingConsutant(ConsutantBooking request)
        {
            try
            {
                var booking = new ConsultationBooking
                {
                    ConsultationBookingId = Guid.NewGuid(),
                    UserId = request.UserId,
                    Datetime = request.BookingDate,
                    ConsultantId = request.ConsutantId,
                    Status = "Pending",
                    Title = request.Title,
                    LinkConsultation = await CommonUtil.CreateMeetingUrl()
                };

                await _context.ConsultationBookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public async Task<List<GetConsulationBookingResponse>> GetConculationBookings(Guid userId)
        {
            var getBookings = await _context.ConsultationBookings
                                             .Include(x => x.Consultant)
                                             .ThenInclude(X => X.User)
                                             .Where(x => x.UserId == userId)
                                             .ToListAsync();

            var mapItems = getBookings.Select(c => new GetConsulationBookingResponse
            {
                ConsultationBookingId = c.ConsultationBookingId,
                Title = c.Title,
                LinkConsultation = c.LinkConsultation,
                Status = c.Status,
                Datetime = c.Datetime,
                Degree = c.Consultant.Degree,
                ExperienceYears = c.Consultant.ExperienceYears,
                Bio = c.Consultant.Bio,
                Avatar = c.Consultant.Avatar,
                FullName = c.Consultant.User.FullName,
                Address = c.Consultant.User.Address,
                Email = c.Consultant.User.Email
            }).ToList();

            return mapItems;
        }
    }
}
