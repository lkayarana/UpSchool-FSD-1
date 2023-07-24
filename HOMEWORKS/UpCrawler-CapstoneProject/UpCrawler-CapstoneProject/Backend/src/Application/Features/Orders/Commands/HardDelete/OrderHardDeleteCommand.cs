using Domain.Common;
using MediatR;

namespace Application.Features.Orders.Commands.HardDelete
{
    public class OrderHardDeleteCommand: IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }
}
