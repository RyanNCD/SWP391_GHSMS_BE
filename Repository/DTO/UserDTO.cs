using System;
using System.ComponentModel.DataAnnotations;

namespace Repository.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [MinLength(5, ErrorMessage = "Mật khẩu phải có ít nhất 5 ký tự")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#]).+$", ErrorMessage = "Mật khẩu phải bắt đầu bằng chữ in hoa và chứa ít nhất một ký tự đặc biệt (!, @, #)")]
        public string Password { get; set; } = null!;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

      
        public string Gender { get; set; }

        public string Address { get; set; }

        public int RoleId { get; set; }

        public DateTime? CreateAt { get; set; }

        public string? Avatar { get; set; }
    }
}
