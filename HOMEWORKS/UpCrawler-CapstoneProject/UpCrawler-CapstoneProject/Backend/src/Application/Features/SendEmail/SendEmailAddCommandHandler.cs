using Application.Common.Interfaces;
using Application.Common.Models.Email;
using MediatR;

namespace Application.Features.SendEmail
{
    public class SendEmailAddCommandHandler : IRequestHandler<SendEmailAddCommand, SendEmailAddDto>
    {
        private readonly IEmailService _emailService;

        public SendEmailAddCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<SendEmailAddDto> Handle(SendEmailAddCommand request, CancellationToken cancellationToken)
        {
            var fullName = $"{request.FirstName} {request.LastName}";
            _emailService.SendEmailConfirmation(new SendEmailConfirmationDto()
            {
                Email = request.Email,
                Name = request.FirstName,
                ExcelFile = request.ExcelFile,

            });

            return new SendEmailAddDto(request.FirstName, request.LastName, request.Email, request.ExcelFile);
        }
    }
}
