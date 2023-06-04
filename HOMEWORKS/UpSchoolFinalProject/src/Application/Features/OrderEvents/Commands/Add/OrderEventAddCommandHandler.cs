using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.OrderEvents.Commands.Add
{
    public class OrderEventAddCommandHandler : IRequestHandler<OrderEventAddCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderEventAddCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<Guid>> Handle(OrderEventAddCommand request, CancellationToken cancellationToken)
        {
            var orderEvent = new OrderEvent()
            {
                OrderId = request.OrderId,
                Status = request.Status,
                CreatedOn = DateTimeOffset.Now

            };

            await _applicationDbContext.OrderEvents.AddAsync(orderEvent, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>("The order was successfully added.");
        }
    }
}
