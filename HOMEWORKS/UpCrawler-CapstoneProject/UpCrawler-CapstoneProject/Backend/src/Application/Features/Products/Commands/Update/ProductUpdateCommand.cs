using Domain.Common;
using MediatR;

namespace Application.Features.Products.Commands.Update
{
    public class ProductUpdateCommand:IRequest<Response<int>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public bool IsOnSale { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
