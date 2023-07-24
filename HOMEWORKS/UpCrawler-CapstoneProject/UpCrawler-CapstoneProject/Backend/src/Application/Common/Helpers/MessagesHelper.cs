using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text;

namespace Application.Common.Helpers
{
    public static class MessagesHelper
    {
        public static class Email
        {
            public static class Confirmation
            {
                public static string Subject => "Thank you for using our UpCrawler app for order processing!";
                public static string OrderMessage => "Your order has been successfully completed! You can see the excel table of your products in the attachment.";

                public static string Name(string firstName) =>
                    $"Hi {firstName}";

                public static string ExcelFile(List<string> excelFile)
                {
                    // Create an HTML table from the list of products
                    var tableBuilder = new StringBuilder();

                    tableBuilder.AppendLine("<table>");
                    tableBuilder.AppendLine("<tr><th>Product ID</th><th>Order ID</th><th>Name</th><th>Is On Sale</th><th>Price</th><th>Sale Price</th><th>Image Path</th></tr>");

                    foreach (var productRow in excelFile)
                    {
                        tableBuilder.AppendLine($"<tr>{productRow}</tr>");
                    }

                    tableBuilder.AppendLine("</table>");

                    return tableBuilder.ToString();
                }

            }
        }
    }
}
