namespace ETicaret.Application.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMessageAsync(string[] toos, string subject, string body, bool isBodyHtml = true);

    }
}
