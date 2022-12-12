namespace ETicaret.Application.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMailAsync(string[] toos, string subject, string body, bool isBodyHtml = true);
        Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
    }
}
