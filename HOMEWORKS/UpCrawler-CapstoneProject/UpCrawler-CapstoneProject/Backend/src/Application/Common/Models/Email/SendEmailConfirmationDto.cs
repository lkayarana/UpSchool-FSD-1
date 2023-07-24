using Domain.Entities;

namespace Application.Common.Models.Email
{
    public class SendEmailConfirmationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Product> ExcelFile { get; set; }
        
    }
}
