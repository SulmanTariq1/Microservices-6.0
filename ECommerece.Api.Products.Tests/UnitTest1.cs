using AutoMapper;
using ECommerece.Api.Products.DB;
using ECommerece.Api.Products.DB.Interfaces;
using ECommerece.Api.Products.DB.Profiles;
using ECommerece.Api.Products.DB.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        //[Fact]
        //public async Task GetProductsReturnsAllProducts()
        //{
        //    var options = new DbContextOptionsBuilder<ProductDbContext>()
        //        .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
        //        .Options; 
        //    var dbContext = new ProductDbContext(options);
        //    CreateProducts(dbContext,1);

        //    var productProfile = new ProductProfile();
        //    var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        //    var mapper = new Mapper(configuration);

        //    var productsProvider = new ProductProvider(dbContext, null, mapper);

        //    var product = await productsProvider.getProductsAsync();
        //    Assert.True(product.IsSuccess);
        //    Assert.True(product.products.Any());
        //    Assert.Null(product.Error);
        //}

        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext, 11);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductProvider(dbContext, null, mapper);

            var product = await productsProvider.getProductAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.product);
            Assert.True(product.product.Id == 1);
            Assert.Null(product.Error);
        }

        //[Fact]
        //public async Task GetProductReturnsProductUsingInvalidId()
        //{
        //    var options = new DbContextOptionsBuilder<ProductDbContext>()
        //        .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingInvalidId))
        //        .Options;
        //    var dbContext = new ProductDbContext(options);
        //    CreateProducts(dbContext,21);

        //    var productProfile = new ProductProfile();
        //    var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        //    var mapper = new Mapper(configuration);

        //    var productsProvider = new ProductProvider(dbContext, null, mapper);

        //    var product = await productsProvider.getProductAsync(-1);
        //    Assert.False(product.IsSuccess);
        //    Assert.Null(product.product);
        //    Assert.NotNull(product.Error);
        //}

        private void CreateProducts(ProductDbContext dbContext, int i)
        {
            int total = i + 10;
            for (i = 1; i <= total; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    price = (decimal)(i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}