using System.ComponentModel.DataAnnotations;

namespace Repository.DTO
{
    public class TestDTO
    {
        public int TestId { get; set; }

        [Required(ErrorMessage = "Tên xét nghiệm là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên xét nghiệm không được vượt quá 100 ký tự")]
        public string Name { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số dương")]
        public decimal Price { get; set; }
    }
}
