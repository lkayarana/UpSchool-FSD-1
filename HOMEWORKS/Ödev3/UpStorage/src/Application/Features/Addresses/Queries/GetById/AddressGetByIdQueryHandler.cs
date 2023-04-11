using Application.Common.Interfaces;
using A = Domain.Entities;
using MediatR;

namespace Application.Features.Addresses.Queries.GetById
{
    public class AddressGetByIdQueryHandler : IRequestHandler<AddressGetByIdQuery, AddressGetByIdDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressGetByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<AddressGetByIdDto> Handle(AddressGetByIdQuery request, CancellationToken cancellationToken)
        {
            A.Address address = await _applicationDbContext.GetByIdAsync(request.Id, false);

            return new()
            {
                Name = address.Name,
                District = address.District,
                PostCode = address.PostCode,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
            };

        }

    }
}
