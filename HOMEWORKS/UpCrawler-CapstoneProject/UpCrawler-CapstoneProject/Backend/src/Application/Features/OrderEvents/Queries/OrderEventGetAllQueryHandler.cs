using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OrderEvents.Queries
{
    public class OrderEventGetAllQueryHandler : IRequestHandler<OrderEventGetAllQuery, List<OrderEventGetAllDto>>
    {
        public readonly IApplicationDbContext _applicationDbContext;

        public OrderEventGetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<OrderEventGetAllDto>> Handle(OrderEventGetAllQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _applicationDbContext.OrderEvents.AsQueryable();
            //dbQuery=dbQuery.Where(x=>x.Id==request.Id);
            dbQuery = dbQuery.Where(x => x.IsDeleted == request.IsDeleted);

            if (request.IsDeleted.HasValue) dbQuery = dbQuery.Where(x => x.IsDeleted == request.IsDeleted.Value);

            var orderEvents = await dbQuery.ToListAsync(cancellationToken);
            var orderEventDtos = MapOrderEventsToGettAllDtos(orderEvents);
            return orderEventDtos.ToList();

        }

        private IEnumerable<OrderEventGetAllDto> MapOrderEventsToGettAllDtos(List<OrderEvent> orderEvents)
        {
            List<OrderEventGetAllDto> orderEventsGetAllDtos = new List<OrderEventGetAllDto>();
            foreach (var orderEvent in orderEvents)
            {

                yield return new OrderEventGetAllDto()
                {
                    Id = orderEvent.Id,
                    OrderId = orderEvent.OrderId,
                    Status = orderEvent.Status,
                    IsDeleted = orderEvent.IsDeleted,
                };
            }

        }
    }
}
