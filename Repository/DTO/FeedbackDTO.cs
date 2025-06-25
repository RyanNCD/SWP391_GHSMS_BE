using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class FeedbackDTO
    {
        public int FeedbackId { get; set; }
        [Required(ErrorMessage = "Người dùng là bắt buộc")]
        public int UserId { get; set; }

        public int? TestBookingId { get; set; }

        public int? ConsultationBookingId { get; set; }
        [Range(1, 5, ErrorMessage = "Đánh giá phải từ 1 đến 5 sao")]
        public int? Rating { get; set; }
        [StringLength(1000, ErrorMessage = "Bình luận không được vượt quá 1000 ký tự")]
        public string? Comment { get; set; }

        public DateTime? CreateAt { get; set; }
    }
}
