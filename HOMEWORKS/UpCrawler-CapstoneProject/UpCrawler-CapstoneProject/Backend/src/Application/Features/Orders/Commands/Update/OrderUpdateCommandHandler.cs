using Application.Common.Interfaces;
using Domain.Common;
using MediatR;

namespace Application.Features.Orders.Commands.Update
{
    public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderUpdateCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public async Task<Response<Guid>> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {

            var order = await _applicationDbContext.Orders.FindAsync(request.Id);

            order.TotalFoundAmount = request.TotalFoundAmount;

            if (order == null)
            {
                return new Response<Guid>("Order not found.");
            }

            if (request.RequestedAmount.ToLower() == "all")
            {
                // Kullanıcı "hepsi" dediyse, tüm miktarı güncelle
                order.RequestedAmount = order.TotalFoundAmount;
            }
            else
            {
                // Kullanıcı sayı girdiyse, girilen sayıyı güncelle
                if (!int.TryParse(request.RequestedAmount, out int requestedAmount))
                {
                    return new Response<Guid>("Invalid requested amount.");
                }

                order.RequestedAmount = requestedAmount;
            }


            _applicationDbContext.Orders.Update(order);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>(order.Id);
        }
    }
}
