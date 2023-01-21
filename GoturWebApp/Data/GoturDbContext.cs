using GoturWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GoturWebApp.Data
{
    public class GoturDbContext : DbContext
    {
        public GoturDbContext(DbContextOptions<GoturDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketDetail> BasketDetail { get; set; }
    }
}
