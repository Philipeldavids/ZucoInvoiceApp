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

    public class EmailService : IEmailService
    {
        public class EmailAttachment
        {
            public string FileName { get; set; }
            public byte[] FileBytes { get; set; }
            public string ContentType { get; set; }

            //public EmailAttachment(string fileName, byte[] fileBytes, string contentType)
            //{
            //    FileName = fileName;
            //    FileBytes = fileBytes;
            //    ContentType = contentType;
            //}
        }
        public async Task SendEmailAsync(string to, string subject, string body, EmailAttachment Attachment = null)
        {
            using var mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("noreply@yourapp.com", "YourApp");
            mail.Attachments.Add(new Attachment(
                   new MemoryStream(Attachment.FileBytes),
                   Attachment.FileName,
                   Attachment.ContentType));




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
