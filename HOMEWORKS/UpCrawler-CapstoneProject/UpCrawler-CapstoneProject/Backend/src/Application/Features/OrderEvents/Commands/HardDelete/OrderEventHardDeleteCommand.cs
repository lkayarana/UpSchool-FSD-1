using Domain.Common;
using MediatR;

namespace Application.Features.OrderEvents.Commands.HardDelete
{
    public class OrderEventHardDeleteCommand:IRequest<Response<Guid>>
    {
        public Guid OrderId { get; set; }
    }
}
