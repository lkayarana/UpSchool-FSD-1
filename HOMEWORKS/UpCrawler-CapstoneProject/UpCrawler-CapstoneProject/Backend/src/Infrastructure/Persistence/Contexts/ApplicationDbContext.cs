using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations.Application;
using Infrastructure.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderEvent> OrderEvents { get; set; }
        public DbSet<NotificationSetting> NotificationSettings { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurations
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEventConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationSettingConfiguration());


            // Seeds
            modelBuilder.ApplyConfiguration(new ProductSeeder());
            modelBuilder.ApplyConfiguration(new OrderSeeder());

            base.OnModelCreating(modelBuilder);
        }
    }
}