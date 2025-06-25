using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class UserRegisterDto
    {
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long")]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9]*[!@#]+.*$",
          ErrorMessage = "Password must start with a capital letter and contain at least one special character (!, @, #)")]
       
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
    }

}
