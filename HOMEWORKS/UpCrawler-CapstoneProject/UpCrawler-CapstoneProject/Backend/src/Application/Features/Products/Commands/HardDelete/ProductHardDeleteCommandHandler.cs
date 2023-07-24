using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.HardDelete
{
    public class ProductHardDeleteCommandHandler : IRequestHandler<ProductHardDeleteCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductHardDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(ProductHardDeleteCommand request, CancellationToken cancellationToken)
        {
            var products = await _applicationDbContext.Products
                .Where(x => x.OrderId == request.OrderId)
                .ToListAsync(cancellationToken);

            if (products.Any())
            {
                _applicationDbContext.Products.RemoveRange(products);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new($"The product was successfully deleted.");
            }


            return new($"The product was not found.");
        }
    }
}
