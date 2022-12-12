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
        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] toos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new("demo@gmailcom", "Demo", Encoding.UTF8);
            foreach (var item in toos) mail.To.Add(item);

            SmtpClient client = new();
            client.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            client.Port = 587;
            client.EnableSsl = true;
            client.Host = _configuration["Mail:Host"];
            await client.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($@"Merhaba <br> Eper yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz
                    .<br>strong<a target='_blank'  href='{_configuration["AngularClientUrl"]}/update-password'");

            builder.AppendLine(userId);
            builder.AppendLine("/");
            builder.AppendLine(resetToken);
            builder.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong>");

            await SendMailAsync(to, "Şifre Yenileme Talebi", builder.ToString(), true);
        }
    }
}
