using Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBookingService
    {
        Task<string> Booking(BookingDTO request);
        Task<List<GetBookingByUserResponse>> GetUserBooking(Guid userId);
        Task<bool> ConsulationBooking(ConsutantBooking request);
        Task<List<GetConsulationBookingResponse>> GetConculationBookings(Guid userId);
    }
}
