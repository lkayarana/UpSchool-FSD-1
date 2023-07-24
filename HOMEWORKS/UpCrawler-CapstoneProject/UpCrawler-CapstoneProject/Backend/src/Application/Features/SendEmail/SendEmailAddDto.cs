using Domain.Entities;

namespace Application.Features.SendEmail
{
    public class SendEmailAddDto
    {
        public SendEmailAddDto(string firstName, string lastName, string email, List<Product> excelFile)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ExcelFile = excelFile;
          
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Product> ExcelFile { get; set; }
       
    }
}
