using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OrderEvents.Commands.HardDelete
{
    public class OrderEventHardDeleteCommandHandler: IRequestHandler<OrderEventHardDeleteCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderEventHardDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Response<Guid>> Handle(OrderEventHardDeleteCommand request, CancellationToken cancellationToken)
        {
            var orderEvents = await _applicationDbContext.OrderEvents
                .Where(x => x.OrderId == request.OrderId)
                .ToListAsync(cancellationToken);

            if (orderEvents.Any())
            {
                _applicationDbContext.OrderEvents.RemoveRange(orderEvents);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new($"The product was successfully deleted.");
            }


            return new($"The product was not found.");
        }
    }
}
