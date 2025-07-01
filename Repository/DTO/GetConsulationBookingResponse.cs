using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class GetConsulationBookingResponse
    {
        public Guid ConsultationBookingId { get; set; }

        public DateTime Datetime { get; set; }

        public string? Title { get; set; }

        public string? LinkConsultation { get; set; }

        public string? Status { get; set; }

        public string Degree { get; set; }
        public int? ExperienceYears { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
