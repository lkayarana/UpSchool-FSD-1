using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Common.Services
{
    public class OrderService:IOrderService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public Task<Guid> AddOrder(Order order)
        {
            _applicationDbContext.Orders.Add(order);
            _applicationDbContext.SaveChanges();
            return null;
        }
    }
}
