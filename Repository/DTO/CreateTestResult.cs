using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class CreateTestResult
    {
        public Guid BookingID { get; set; }
        public string typeSTIs { get; set; }
        public string testSample { get; set; }
        public string testBlood { get; set; }
        public string testUrine { get; set; }
        public string diagnosticResults { get; set; }
    }
}
