using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class UserProfileDTO
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
       
        public string? phoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public DateTime? createdAt {  get; set; }

    }

}
