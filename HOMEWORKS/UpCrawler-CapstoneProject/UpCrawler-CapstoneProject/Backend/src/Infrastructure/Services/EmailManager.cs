using Application.Common.Interfaces;
using Application.Common.Models.Email;
using Application.Common.Helpers;
using System.Net;
using System.Net.Mail;
using static Domain.Utilities.SignalRMethodKeys;
using Domain.Entities;
using System.Text;

namespace Infrastructure.Services
{
    public class EmailManager : IEmailService
    {
        private readonly string _wwwrootPath;

        public EmailManager(string wwwrootPath)
        {
            _wwwrootPath = wwwrootPath;
        }

        public void SendEmailConfirmation(SendEmailConfirmationDto sendEmailConfirmationDto)
        {
            var htmlContent = File.ReadAllText($"{_wwwrootPath}/email_templates/email_confirmation.html");

            htmlContent = htmlContent.Replace("{{subject}}", MessagesHelper.Email.Confirmation.Subject);

            htmlContent = htmlContent.Replace("{{name}}",
                MessagesHelper.Email.Confirmation.Name(sendEmailConfirmationDto.Name));

            // Generate the HTML table content from the list of products
            var excelFileContent = GenerateExcelFileContent(sendEmailConfirmationDto.ExcelFile);

            // Replace the {{excelFile}} placeholder with the generated HTML table content
            htmlContent = htmlContent.Replace("{{excelFile}}", excelFileContent);



            htmlContent = htmlContent.Replace("{{activationMessage}}",
                MessagesHelper.Email.Confirmation.OrderMessage);

            Send(new SendEmailDto(sendEmailConfirmationDto.Email, htmlContent,
                MessagesHelper.Email.Confirmation.Subject));

        }

        private void Send(SendEmailDto sendEmailDto)
        {
            MailMessage message = new MailMessage();

            sendEmailDto.EmailAddresses.ForEach(emailAddress => message.To.Add(emailAddress));

            message.From = new MailAddress("upcrawler1@outlook.com");

            message.Subject = sendEmailDto.Subject;

            message.IsBodyHtml = true;

            message.Body = sendEmailDto.Content;


            SmtpClient client = new SmtpClient();

            client.Port = 587;

            client.Host = "smtp.sendgrid.net";

            client.EnableSsl = true;

            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential("apikey", "");

            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Send(message);
        }

        private string GenerateExcelFileContent(List<Product> products)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<table role=\"presentation\" border=\"1\" cellpadding=\"10\" cellspacing=\"0\">");
            htmlBuilder.AppendLine("<thead>");
            htmlBuilder.AppendLine("<tr>");
            htmlBuilder.AppendLine("<th>Product ID</th>");
            htmlBuilder.AppendLine("<th>Order ID</th>");
            htmlBuilder.AppendLine("<th>Name</th>");
            htmlBuilder.AppendLine("<th>Is On Sale</th>");
            htmlBuilder.AppendLine("<th>Price</th>");
            htmlBuilder.AppendLine("<th>Sale Price</th>");
            htmlBuilder.AppendLine("<th>Image Path</th>");
            htmlBuilder.AppendLine("</tr>");
            htmlBuilder.AppendLine("</thead>");
            htmlBuilder.AppendLine("<tbody>");

            foreach (var product in products)
            {
                htmlBuilder.AppendLine("<tr>");
                htmlBuilder.AppendLine($"<td>{product.Id}</td>");
                htmlBuilder.AppendLine($"<td>{product.OrderId}</td>");
                htmlBuilder.AppendLine($"<td>{product.Name}</td>");
                htmlBuilder.AppendLine($"<td>{product.IsOnSale}</td>");
                htmlBuilder.AppendLine($"<td>{product.Price}</td>");
                htmlBuilder.AppendLine($"<td>{product.SalePrice}</td>");
                htmlBuilder.AppendLine($"<td>{product.Picture}</td>");
                htmlBuilder.AppendLine("</tr>");
            }

            htmlBuilder.AppendLine("</tbody>");
            htmlBuilder.AppendLine("</table>");

            return htmlBuilder.ToString();
        }


    }
}