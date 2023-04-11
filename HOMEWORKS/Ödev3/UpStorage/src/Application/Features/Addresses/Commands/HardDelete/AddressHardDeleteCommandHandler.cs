using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Addresses.Commands.HardDelete
{
    public class AddressHardDeleteCommandHandler : IRequestHandler<AddressHardDeleteCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressHardDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Response<int>> Handle(AddressHardDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Addresses
                .Where(x => x.Id == Guid.Parse(request.Id))
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new Exception("The address was not found.");
            }

            _applicationDbContext.Addresses.Remove(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new($"The address named \"{entity.Name}\" was successfully deleted.");
        }

    }
}
