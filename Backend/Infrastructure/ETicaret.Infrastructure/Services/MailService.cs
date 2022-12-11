using ETicaret.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ETicaret.Infrastructure.Services
{
    public class MailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMessageAsync(string[] toos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], _configuration["Mail:Password"], Encoding.UTF8);
            foreach (var item in toos) mail.To.Add(item);

            SmtpClient client = new();
            client.Credentials = new NetworkCredential("", "");
            client.Port = 587;
            client.EnableSsl = true;
            client.Host = _configuration["Mail:Host"];
            await client.SendMailAsync(mail);
        }
    }
}
