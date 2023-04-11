using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class AddressConfiguration:IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {

            //Configurations added.

            // ID
            builder.HasKey(x => x.Id);

            // Name
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(150);
            builder.HasIndex(x => x.Name);

            //District
            builder.Property(x => x.District).IsRequired();
            builder.Property(x => x.District).HasMaxLength(50);

            // PostCode
            builder.Property(c => c.PostCode).IsRequired();
            builder.Property(c => c.PostCode).HasMaxLength(20);

            //AddressLine1
            builder.Property(c => c.AddressLine1).IsRequired();
            builder.Property(c => c.AddressLine1).HasMaxLength(100);

            //AddressLine 2
            builder.Property(c => c.AddressLine2).IsRequired(false);
            builder.Property(c => c.AddressLine2).HasMaxLength(100);

            // AddressType
            builder.Property(x => x.AddressType).IsRequired();
            builder.Property(x => x.AddressType).HasConversion<int>();

            // CreatedByUserId
            builder.Property(x => x.CreatedByUserId).IsRequired(false);
            builder.Property(x => x.CreatedByUserId).HasMaxLength(100);

            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            // ModifiedByUserId
            builder.Property(x => x.ModifiedByUserId).IsRequired(false);
            builder.Property(x => x.ModifiedByUserId).HasMaxLength(100);

            // ModifiedOn
            builder.Property(x => x.ModifiedOn).IsRequired(false);

            // DeletedByUserId
            builder.Property(x => x.DeletedByUserId).IsRequired(false);
            builder.Property(x => x.DeletedByUserId).HasMaxLength(100);

            // DeletedOn
            builder.Property(x => x.DeletedOn).IsRequired(false);

            // IsDeleted
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("0");

            // Relationships added. 
            builder.HasOne<User>(x => x.User)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.UserId);

            builder.ToTable("Addresses");
        }
    }
}
