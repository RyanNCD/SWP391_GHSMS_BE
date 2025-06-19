using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class ConsultantDTO
    {
        public int Id { get; set; }
        public string degree { get; set; }
        public int ExperienceYears { get; set; }
        public string bio { get; set; }
        public string avatar { get; set; }
    }
}
