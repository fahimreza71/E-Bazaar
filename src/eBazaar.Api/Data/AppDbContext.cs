using eBazaar.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace eBazaar.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
    }
}
