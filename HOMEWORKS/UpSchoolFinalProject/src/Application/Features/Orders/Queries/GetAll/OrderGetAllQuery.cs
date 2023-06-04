using MediatR;

namespace Application.Features.Orders.Queries.GetAll
{
    public class OrderGetAllQuery : IRequest<List<OrderGetAllDto>>
    {
        public OrderGetAllQuery(bool? isDeleted)
        {
            IsDeleted = isDeleted;
        }

        public bool? IsDeleted { get; set; }
    }
}
