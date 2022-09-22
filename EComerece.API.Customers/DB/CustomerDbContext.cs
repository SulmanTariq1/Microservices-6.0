using Microsoft.EntityFrameworkCore;

namespace EComerece.API.Customers.DB
{
    public class CustomerDbContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomerDbContext(DbContextOptions options): base(options)
        {

        }
    }
}
