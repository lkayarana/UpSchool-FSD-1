using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Seeders
{
    public class ProductSeeder : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(GetInitialProducts());
        }

        private List<Product> GetInitialProducts()
        {
            return new List<Product>();
        }
    }
}
