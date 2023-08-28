using BathroomRenovation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BathroomRenovation.Data
{
    public class BathroomRenovationDbContext : DbContext
    {
        public BathroomRenovationDbContext(DbContextOptions<BathroomRenovationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<BathroomItem> BathroomItems { get; set; }

        public DbSet<BathroomItemBrand> BathroomItemBrands { get; set; }

        public DbSet<BathroomItemOrder> BathroomItemOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BathroomItem>()
                .HasMany(e => e.BathroomItemOrders)
                .WithOne(e => e.BathroomItem)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
