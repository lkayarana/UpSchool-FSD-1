using Domain.Common;
using MediatR;

namespace Application.Features.Products.Commands.Add
{
    public class ProductAddCommand:IRequest<Response<Guid>>, IRequest<Guid>
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public bool IsOnSale { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
