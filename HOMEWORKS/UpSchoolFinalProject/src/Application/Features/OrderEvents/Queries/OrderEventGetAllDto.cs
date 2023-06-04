using Domain.Enums;

namespace Application.Features.OrderEvents.Queries
{
    public class OrderEventGetAllDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
