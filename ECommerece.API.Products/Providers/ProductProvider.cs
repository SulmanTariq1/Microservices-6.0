using AutoMapper;
using ECommerece.Api.Products.DB.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerece.Api.Products.DB.Providers
{
    public class ProductProvider : IProductProvider
    {
        public ProductDbContext DbContext { get; }
        public ILogger<ProductProvider> Logger { get; }
        public IMapper Mapper { get; }

        public ProductProvider(ProductDbContext dbContext,ILogger<ProductProvider> logger, IMapper mapper)
        {
            DbContext = dbContext;
            Logger = logger;
            Mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!DbContext.Products.Any())
            {
                DbContext.Products.Add(new DB.Product() { Id = 1, Name = "Keybord", price = 20, Inventory = 100 });
                DbContext.Products.Add(new DB.Product() { Id = 2, Name = "Mouse", price = 100,Inventory = 120 });
                DbContext.Products.Add(new DB.Product() { Id = 3, Name = "headPhone", price = 120, Inventory = 800 });
                DbContext.Products.Add(new DB.Product() { Id = 4, Name = "usb", price = 200, Inventory = 1000 });
                DbContext.SaveChanges();
            }
        }


        public async Task<(bool IsSuccess, IEnumerable<Models.Product>? products, string Error)> getProductsAsync()
        {
            try
            {
                var products = await DbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var restult = Mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
                    return (true, restult, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product? product, string Error)> getProductAsync(int id)
        {
            var product = await DbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            try
            {
                if (product != null)
                {
                    var restult = Mapper.Map<DB.Product, Models.Product>(product);
                    return (true, restult, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
