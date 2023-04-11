using Domain.Common;
using MediatR;

namespace Application.Features.Addresses.Commands.HardDelete
{
    public class AddressHardDeleteCommand:IRequest<Response<int>>
    {
        public string Id{ get; set; }
    }
}
