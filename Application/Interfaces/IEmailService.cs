using Application.DTOs;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(EmailRequest dto);
        Task SendEmail(EmailRequest dto);
    }
}
