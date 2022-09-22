using AutoMapper;
using EComerece.API.Customers.DB;
using EComerece.API.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EComerece.API.Customers.Providers
{
    public class CustomersProvider : ICustomerProvider
    {

        public CustomerDbContext DbContext { get; }
        public ILogger<CustomersProvider> Logger { get; }
        public IMapper Mapper { get; }
        public CustomersProvider(CustomerDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            DbContext = dbContext;
            Logger = logger;
            Mapper = mapper;
            SeedData();
        }
        private void SeedData()
        {
            if (!DbContext.Customers.Any())
            {
                DbContext.Customers.Add(new DB.Customer() { Id = 1, Name = "Jessica Smith", Address = "20 Elm St." });
                DbContext.Customers.Add(new DB.Customer() { Id = 2, Name = "John Smith", Address = "30 Main St." });
                DbContext.Customers.Add(new DB.Customer() { Id = 3, Name = "William Johnson", Address = "100 10th St." });
                DbContext.SaveChanges();
            }
        }

        async Task<(bool IsSuccess, IEnumerable<Models.Customer>? Customers, string? ErrorMessage)> ICustomerProvider.GetCustomersAsync()
        {
            try
            {
                Logger?.LogInformation("Querying customers");
                var customers = await DbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    Logger?.LogInformation($"{customers.Count} customer(s) found");
                    var result = Mapper.Map<IEnumerable<DB.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        async Task<(bool IsSuccess, Models.Customer? Customer, string? ErrorMessage)> ICustomerProvider.GetCustomerAsync(int id)
        {
            try
            {
                Logger?.LogInformation("Querying customers");
                var customer = await DbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    Logger?.LogInformation($"{customer.Name} customer(s) found");
                    var result = Mapper.Map<DB.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
