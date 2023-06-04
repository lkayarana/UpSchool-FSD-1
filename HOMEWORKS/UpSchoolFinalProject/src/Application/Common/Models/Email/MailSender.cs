using System.Net;
using System.Net.Mail;

namespace Application.Common.Models.Email
{
    public class MailSender
    {
        public void SendEmailWithExcelAttachment(string recipientEmail, string subject, string body, string attachmentFilePath)
        {
            // Configuring email information
            string senderEmail = "noreply@entegraturk.com";
            string senderPassword = "xzx2xg4Jttrbzm5nIJ2kj1pE4l";
            string smtpHost = "mail.entegraturk.com";
            int smtpPort = 587;

            // Creating SMTP client for sending email
            SmtpClient client = new SmtpClient(smtpHost, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            // Create an e-mail
            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);

            // Adding the Excel file
            Attachment attachment = new Attachment(attachmentFilePath);

            mailMessage.Attachments.Add(attachment);

            // Sending the email
            client.Send(mailMessage);
        }
    }
}
