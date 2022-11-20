using Microsoft.EntityFrameworkCore;
using OrderAPI.Models.Entities;

namespace OrderAPI.OrderDbContext
{
    public class OrderApiDbContext : DbContext
    {
        public OrderApiDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>().HasOne(x=>x.Product).WithMany(x=>x.OrderDetails).HasForeignKey(x=>x.ProductId);
            modelBuilder.Entity<OrderDetails>().HasOne(x => x.Order).WithMany(p=>p.OrderDetails).HasForeignKey(x=>x.OrderId);
        }
    }
}
