using Domain.Common;
using MediatR;

namespace Application.Features.Products.Commands.HardDelete
{
    public class ProductHardDeleteCommand: IRequest<Response<int>>
    {
        public Guid OrderId { get; set; }
    }
}
