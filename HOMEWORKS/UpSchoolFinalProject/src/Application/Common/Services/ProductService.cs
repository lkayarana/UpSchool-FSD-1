using Application.Common.Interfaces;
using Domain.Entities;



namespace Application.Common.Services
{
    public class ProductService:IProductService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<Guid> AddProduct(Product product)
        {
            _applicationDbContext.Products.Add(product);
           _applicationDbContext.SaveChanges();
           return null;
        }
    }
}
