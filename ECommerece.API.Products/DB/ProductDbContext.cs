using Microsoft.EntityFrameworkCore;

namespace ECommerece.Api.Products.DB
{
    public class ProductDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
