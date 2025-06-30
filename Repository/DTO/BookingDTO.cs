using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class BookingDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Guid UserId { get; set; }
        public Guid TestId { get; set; }
    }
}
