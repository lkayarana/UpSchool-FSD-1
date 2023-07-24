using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Update
{
    public class ProductUpdateCommandHandler: IRequestHandler<ProductUpdateCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductUpdateCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public async Task<Response<int>> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _applicationDbContext.Products
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                throw new Exception();
            }

            product.Id = request.Id;
            product.Name = request.Name;
            product.Picture = request.Picture;
            product.Price = request.Price;
            product.SalePrice = request.SalePrice;

            _applicationDbContext.Products.Update(product);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new($"The product was successfully updated.");
        }
    }
}
