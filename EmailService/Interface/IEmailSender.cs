using EmailService.DTO;

namespace EmailService.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailRequest<string> request);
    }
}
