using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class NotificationSettingConfiguration : IEntityTypeConfiguration<NotificationSetting>
    {
        public void Configure(EntityTypeBuilder<NotificationSetting> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Message)
                .IsRequired();

            builder.Property(o => o.Options)
                .IsRequired();

            builder.Property(o => o.Timestamp)
                .IsRequired();

            builder.ToTable("NotificationSettings");

        }
    }
}
