using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RequestedAmount)
                .IsRequired();

            builder.Property(x => x.TotalFoundAmount)
                .IsRequired();

            builder.Property(x => x.ProductCrawlType)
                .IsRequired();

            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.ToTable("Orders");

            builder.HasMany(x => x.OrderEvents)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Products)
                .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}