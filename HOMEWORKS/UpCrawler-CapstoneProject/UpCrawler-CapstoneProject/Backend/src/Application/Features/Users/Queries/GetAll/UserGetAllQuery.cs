using Domain.Identity;
using MediatR;

namespace Application.Features.Users.Queries.GetAll
{
    public class UserGetAllQuery : IRequest<List<UserGetAllDto>>
    {
        public UserGetAllQuery(bool? isDeleted)
        {
            IsDeleted = isDeleted;
        }

        public bool? IsDeleted { get; set; }
    }
}
