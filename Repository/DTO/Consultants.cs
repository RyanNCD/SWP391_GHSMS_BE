using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class Consultants
    {
        //public int Id { get; set; }

        [Required(ErrorMessage = "Bằng cấp là bắt buộc")]
        [StringLength(100, ErrorMessage = "Bằng cấp không được vượt quá 100 ký tự")]
        public string degree { get; set; }

        [Range(0, 100, ErrorMessage = "Số năm kinh nghiệm phải từ 0 đến 100")]
        public int ExperienceYears { get; set; }

        [Required(ErrorMessage = "Tiểu sử là bắt buộc")]
        [StringLength(1000, ErrorMessage = "Tiểu sử không được vượt quá 1000 ký tự")]
        public string bio { get; set; }

        [Url(ErrorMessage = "Ảnh đại diện phải là một URL hợp lệ")]
        public string avatar { get; set; }
    }
}
