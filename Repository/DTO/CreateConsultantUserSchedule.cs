using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class CreateConsultantUserSchedule
    {
        public Guid ConsutantId { get; set; }
        public DateTime scheduleDateTime { get; set; }
        public int durationMinutes { get; set; }
        public string status { get; set; }
        public string notes { get; set; }
        public DateTime createAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Guid userId { get; set; }
    }
}
