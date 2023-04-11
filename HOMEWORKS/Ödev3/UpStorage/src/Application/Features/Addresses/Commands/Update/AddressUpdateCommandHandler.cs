using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Addresses.Commands.Update
{
    public class AddressUpdateCommandHandler : IRequestHandler<AddressUpdateCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressUpdateCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public async Task<Response<int>> Handle(AddressUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Addresses
                .Where(x => x.Id == Guid.Parse(request.Id))
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new Exception();
            }


            entity.Name = request.Name;
            entity.UserId = request.UserId;
            entity.CountryId = request.CountryId;
            entity.CityId = request.CityId;
            entity.District = request.District;
            entity.PostCode = request.PostCode;
            entity.AddressLine1 = request.AddressLine1;
            entity.AddressLine2 = request.AddressLine2;



            _applicationDbContext.Addresses.Update(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new($"The address named \"{entity.Name}\" was successfully updated.");
        }
    }
}