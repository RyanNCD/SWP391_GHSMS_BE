using EmailService.Config;
using EmailService.DTO;
using System.Net.Mail;
using System.Net;
using EmailService.Interface;
using Microsoft.Extensions.Options;

namespace EmailService.Implement
{
    public class EmailSender : IEmailSender
    {
        private readonly SendMailConfig _config;
        public EmailSender(IOptions<SendMailConfig> config)
        {
            _config = config.Value;
        }
        public async Task SendEmailAsync(EmailRequest<string> request)
        {
            using var client = new SmtpClient(_config.SmtpServer, _config.Port)
            {
                Credentials = new NetworkCredential(_config.Username, _config.Password),
                EnableSsl = true,
                UseDefaultCredentials = false
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_config.SenderEmail, _config.SenderName),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(request.To);

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                throw new Exception($"Gửi email thất bại: {ex.StatusCode} - {ex.Message}", ex);
            }
        }
    }
}
