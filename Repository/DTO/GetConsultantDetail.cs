using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class GetConsultantDetail
    {
        public Guid consultantId { get; set; }
        public string Degree { get; set; }
        public int? ExperienceYears { get; set; }
        public string Bio { get; set; }
        public UserDTO User { get; set; }
    }
}
