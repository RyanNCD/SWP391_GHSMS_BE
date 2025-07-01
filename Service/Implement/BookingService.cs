using Repository.DTO;
using Repository.Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class BookingService(BookingRepository _bookingRepository) : IBookingService
    {
        public async Task<string> Booking(BookingDTO request)
        {
            return await _bookingRepository.Booking(request);
        }

        public async Task<bool> ConsulationBooking(ConsutantBooking request)
        {
            return await _bookingRepository.BookingConsutant(request);
        }

        public async Task<List<GetConsulationBookingResponse>> GetConculationBookings(Guid userId)
        {
            return await _bookingRepository.GetConculationBookings(userId);
        }

        public async Task<List<GetBookingByUserResponse>> GetUserBooking(Guid userId)
        {
            return await _bookingRepository.GetBookingByUser(userId);
        }
    }
}
