using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Delete
{
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var product = await _applicationDbContext.Products
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                throw new Exception("The product was not found.");
            }

            product.IsDeleted = true;

            _applicationDbContext.Products.Update(product);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new($"The product was successfully deleted.");
        }
    }
}
