using Application.Common.Models.Email;
using Domain.Entities;
using OfficeOpenXml;


namespace Application.Common.Models.Excel
{
    public class ExcelModel
    {
        public void ExportToExcel(List<Product> productList, string recipientEmail, string subject, string body)
        {
            // Create a new Excel package
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                // Create a new workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Products");

                // Add header line
                worksheet.Cells[1, 1].Value = "OrderId";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Price";
                worksheet.Cells[1, 4].Value = "Sale Price";
                worksheet.Cells[1, 5].Value = "IsOnSale";
                worksheet.Cells[1, 6].Value = "Picture";

                // Fill products into Excel table

                for (int i = 0; i < productList.Count; i++)
                {
                    Product product = productList[i];

                    worksheet.Cells[i + 2, 1].Value = product.OrderId;
                    worksheet.Cells[i + 2, 2].Value = product.Name;
                    worksheet.Cells[i + 2, 3].Value = product.Price;
                    worksheet.Cells[i + 2, 4].Value = product.SalePrice;
                    worksheet.Cells[i + 2, 5].Value = product.IsOnSale;
                    worksheet.Cells[i + 2, 6].Value = product.Picture;
                }

                // Save the file

                string fileName = "products.xlsx";

                string filePath = Path.Combine(@"C:\Users\ranak\source\repos\UpSchoolFinalProject\src\Application\Common\Models\Excel\", fileName);

                FileInfo fileInfo = new FileInfo(filePath);

                package.SaveAs(fileInfo);

                // Email Excel file

                MailSender mailSender = new MailSender();

                mailSender.SendEmailWithExcelAttachment(recipientEmail, subject, body, filePath);
            }
        }
    }
}

