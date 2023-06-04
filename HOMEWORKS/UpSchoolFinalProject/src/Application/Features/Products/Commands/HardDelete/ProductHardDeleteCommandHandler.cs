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
            var product = await _applicationDbContext.Products
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                throw new Exception("The product was not found.");
            }

            _applicationDbContext.Products.Remove(product);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new($"The product was successfully deleted.");
        }
    }
}
