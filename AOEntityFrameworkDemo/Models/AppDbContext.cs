using AOEntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AOEntityFrameworkDemo.Models
{
    public class Product :BaseEntity
    {
        public string Name { get { return GetProperty<string>(); } set { SetProperty<string>(value); } }
        public decimal Price { get { return GetProperty<decimal>(); } set { SetProperty<decimal>(value); } }
    }
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

       public DbSet<Product> Products { get; set;}
    }
}
