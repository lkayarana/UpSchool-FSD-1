using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<Guid> AddProduct(Product product);
    }
}
