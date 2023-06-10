using System.Net;
using System.Net.Mail;

namespace Application.Common.Models
{
    public class MailSender
    {
        public void SendEmailWithExcelAttachment(string recipientEmail, string subject, string body, string attachmentFilePath)
        {
            // E-posta bilgilerini yapılandırma
            string senderEmail = "noreply@entegraturk.com";
            string senderPassword = "xzx2xg4Jttrbzm5nIJ2kj1pE4l";
            string smtpHost = "mail.entegraturk.com";
            int smtpPort = 587;

            // E-posta gönderme işlemi için SMTP istemcisi oluşturma
            SmtpClient client = new SmtpClient(smtpHost, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            // E-posta oluşturma
            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);

            // Excel dosyasını ekleme
            Attachment attachment = new Attachment(attachmentFilePath);
            mailMessage.Attachments.Add(attachment);

            // E-postayı gönderme
            client.Send(mailMessage);
        }
    }
}
