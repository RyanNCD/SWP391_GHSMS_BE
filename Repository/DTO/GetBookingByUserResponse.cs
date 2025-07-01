using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class GetBookingByUserResponse
    {
        public Guid TestBookingId { get; set; }
        public DateTime scheduledDate { get; set; }
        public string Status { get; set; }
    }
}
