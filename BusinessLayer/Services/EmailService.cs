using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("noreply@yourapp.com", "YourApp");

            using var smtp = new SmtpClient("smtp.yourprovider.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("your-email", "your-password"),
                EnableSsl = true,
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
