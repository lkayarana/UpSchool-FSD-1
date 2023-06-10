using Domain.Entities;
using OfficeOpenXml;


namespace Application.Common.Models
{
    public class ExcelModel
    {
        public void ExportToExcel(List<Product> products)
        {
            // Yeni bir Excel paketi oluştur
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                // Yeni bir çalışma kitabı oluştur
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Products");

                // Başlık satırını ekle
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Price";
                worksheet.Cells[1, 3].Value = "Sale Price";
                worksheet.Cells[1, 4].Value = "IsOnSale";
                worksheet.Cells[1, 5].Value = "Picture";

                // Ürünleri Excel tablosuna doldur
                for (int i = 0; i < products.Count; i++)
                {
                    Product product = products[i];
                    worksheet.Cells[i + 2, 1].Value = product.Name;
                    worksheet.Cells[i + 2, 2].Value = product.Price;
                    worksheet.Cells[i + 2, 3].Value = product.SalePrice;
                    worksheet.Cells[i + 2, 4].Value = product.IsOnSale;
                    worksheet.Cells[i + 2, 5].Value = product.Picture;
                }

                // Dosyayı kaydet
                FileInfo fileInfo = new FileInfo("products.xlsx");
                package.SaveAs(fileInfo);
            }
        }
    }
}

