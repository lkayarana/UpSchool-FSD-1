using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> AddOrder(Order order);
    }
}
