using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class ConsutantBooking
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Guid UserId { get; set; }
        public Guid ConsutantId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
    }
}
