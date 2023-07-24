using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetAll
{
    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, List<ProductGetAllDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductGetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<ProductGetAllDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _applicationDbContext.Products.AsQueryable();

            if (request.IsDeleted.HasValue)
            {
                dbQuery = dbQuery.Where(x => x.IsDeleted == request.IsDeleted.Value);
            }

            var products = await dbQuery.ToListAsync(cancellationToken);
            var productDtos = MapProductsToGetAllDtos(products);
            return productDtos;
        }

        private List<ProductGetAllDto> MapProductsToGetAllDtos(List<Product> products)
        {
            List<ProductGetAllDto> productGetAllDtos = new List<ProductGetAllDto>();

            foreach (var product in products)
            {
                productGetAllDtos.Add(new ProductGetAllDto()
                {
                    Id = product.Id,
                    OrderId = product.OrderId,
                    Name = product.Name,
                    Picture = product.Picture,
                    IsOnSale = product.IsOnSale,
                    Price = product.Price,
                    SalePrice = product.SalePrice,
                    IsDeleted = product.IsDeleted,
                });
            }

            return productGetAllDtos;
        }
    }
}