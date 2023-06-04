using MediatR;

namespace Application.Features.Products.Queries.GetAll
{
    public class ProductGetAllQuery : IRequest<List<ProductGetAllDto>>
    {
        public ProductGetAllQuery(bool? isDeleted)
        {
            IsDeleted = isDeleted;
        }

        public bool? IsDeleted { get; set; }
    }
}
