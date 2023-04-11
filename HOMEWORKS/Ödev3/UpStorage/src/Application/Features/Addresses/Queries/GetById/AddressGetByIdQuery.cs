using MediatR;

namespace Application.Features.Addresses.Queries.GetById
{
    public class AddressGetByIdQuery:IRequest<AddressGetByIdDto>
    {
        public string Id { get; set; }
    }
}
