using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class TestResultResponse
    {
        public Guid ResultId { get; set; }
        public string TypeSTIs { get; set; }
        public string TestSample { get; set; }
        public string TestBlood { get; set; }
        public string TestUrine { get; set; }
        public string DiagnosticResults { get; set; }

    }

    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
    }
}
