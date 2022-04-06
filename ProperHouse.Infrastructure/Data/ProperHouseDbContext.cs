using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Infrastructure.Data
{
    public class ProperHouseDbContext : IdentityDbContext
    {
        public ProperHouseDbContext(DbContextOptions<ProperHouseDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Property>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Properties)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            

            builder
                .Entity<Reservation>()
                .HasOne(r => r.Property)
                .WithMany(r => r.Reservations)
                .HasForeignKey(r => r.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }

        public DbSet<Property> Properties { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

    }
}