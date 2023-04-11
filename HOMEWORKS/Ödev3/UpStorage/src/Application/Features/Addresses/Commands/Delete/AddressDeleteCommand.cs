using Domain.Common;
using MediatR;

namespace Application.Features.Addresses.Commands.Delete
{
    public class AddressDeleteCommand:IRequest<Response<int>>
    {
        public string Id { get; set; }
    }
}
