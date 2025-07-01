namespace Repository.DTO;

public class UserMessageDto
{
    public Guid MessageId { get; set; }
    public string Message { get; set; }
    public DateTime? SentAt { get; set; }
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set; }
}