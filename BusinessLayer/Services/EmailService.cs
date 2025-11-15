using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static BusinessLayer.Services.EmailService;

namespace BusinessLayer.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, EmailAttachment Attachment = null);
    }

    public class EmailAttachment
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
        public string ContentType { get; set; }

    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        //private string host;
        //private string Email;
        //private string Password;
        //private int PortNumber;
        public EmailService(IConfiguration config)
        {
            _config = config;
        
        }
        public async Task SendEmailAsync(string to, string subject, string body, EmailAttachment Attachment = null)
        {
            using var mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(_config["EmailSettings:Email"], "ZucoInvoice");
            mail.Attachments.Add(new Attachment(
                   new MemoryStream(Attachment.FileBytes),
                   Attachment.FileName,
                   Attachment.ContentType));




            using var smtp = new SmtpClient(_config["EmailSettings:Host"])
            {
                Port = Convert.ToInt32(_config["EmailSettings:port"]),
                Credentials = new System.Net.NetworkCredential(_config["EmailSettings:Email"], _config["EmailSettings:Password"]),
                EnableSsl = true,
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
