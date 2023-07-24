//using Domain.Identity;
//using MediatR;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace Application.Features.Users.Queries.GetAll
//{
//    public class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, List<UserGetAllDto>>
//    {
//        private readonly IdentityDbContext _identityDbContext;

//        public UserGetAllQueryHandler(IdentityDbContext dbContext)
//        {
//            _identityDbContext = dbContext;
//        }

//        public async Task<List<UserGetAllDto>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
//        {
            
//            var dbQuery = _identityDbContext.Users.AsQueryable();
//            var users = await dbQuery.ToListAsync(cancellationToken);
//            var userDtos = MapProductsToGetAllDtos(users);
//            return userDtos;

//        }

//        private List<UserGetAllDto> MapProductsToGetAllDtos(List<User> users)
//        {
//            List<UserGetAllDto> userGetAllDtos = new List<UserGetAllDto>();

//            foreach (var user in users)
//            {
//                userGetAllDtos.Add(new UserGetAllDto()
//                {
//                    FirstName = user.FirstName,
//                    LastName = user.LastName,
//                    Email = user.Email,
//                });
//            }

//            return userGetAllDtos;
//        }


//    }
//}