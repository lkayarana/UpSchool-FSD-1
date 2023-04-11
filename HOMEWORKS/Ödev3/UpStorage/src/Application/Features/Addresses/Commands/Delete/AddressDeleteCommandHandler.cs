using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Addresses.Commands.Delete
{
    public class AddressDeleteCommandHandler : IRequestHandler<AddressDeleteCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(AddressDeleteCommand request, CancellationToken cancellationToken)
        {

            var entity = await _applicationDbContext.Addresses
                .Where(x => x.Id == Guid.Parse(request.Id))
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new Exception("The address was not found.");
            }

            entity.IsDeleted = true;

            _applicationDbContext.Addresses.Update(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new($"The address named \"{entity.Name}\" was successfully deleted.");
        }
    }
}
