using CURDAPI.Models;
using E_Commerce_Backend_System.Models.Entityes;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce_Backend_System.Models.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Users> Users { get; set; }
       
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
    }
}
