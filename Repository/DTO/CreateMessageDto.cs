using System.ComponentModel.DataAnnotations;

namespace Repository.DTO;

public class CreateMessageDto
{
    [Required]
    public string ReceiverEmail { get; set; }
    [Required]
    public string Message { get; set; }
}