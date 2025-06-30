using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class TestDetailResponse
    {
        public TestResponse Test { get; set; }
        //public ConsutantResponse Consutant { get; set; }

    }

    public class ConsutantResponse
    {
        public Guid ConsutantId { get; set; }
        public string Degree { get; set; }
        public int? experienceYears { get; set; }
        public string Bio { get; set; }
        public string Avartar { get; set; }
    }
}
