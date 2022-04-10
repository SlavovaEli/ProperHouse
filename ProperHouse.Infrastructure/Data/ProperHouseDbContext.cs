using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Infrastructure.Data
{
    public class ProperHouseDbContext : IdentityDbContext<User>
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
                .Entity<Property>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Properties)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            

            builder
                .Entity<Reservation>()
                .HasOne(r => r.Property)
                .WithMany(p => p.Reservations)
                .HasForeignKey(r => r.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Reservation>()
                .HasOne<User>()                
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            

            builder
                .Entity<Owner>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Owner>(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);




            base.OnModelCreating(builder);
        }

        public DbSet<Property> Properties { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

    }
}