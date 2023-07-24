using Domain.Entities;
using MediatR;

namespace Application.Features.SendEmail
{
    public class SendEmailAddCommand : IRequest<SendEmailAddDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Product> ExcelFile { get; set; }
        
    }
}
