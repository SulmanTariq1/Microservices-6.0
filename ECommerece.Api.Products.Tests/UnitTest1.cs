using AutoMapper;
using ECommerece.Api.Products.DB;
using ECommerece.Api.Products.DB.Interfaces;
using ECommerece.Api.Products.DB.Profiles;
using ECommerece.Api.Products.DB.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerece.Api.Products.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnAllProducts)).Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            var productprofile = new ProductProfile();
            var config= new MapperConfiguration(cfg=> cfg.AddProfile(productprofile));
            var mapper = new Mapper(config);

            var productProvider = new ProductProvider(dbContext, null, mapper);
            var products = await productProvider.getProductsAsync();


            Assert.True(products.IsSuccess);
            Assert.True(products.products.Any());
            Assert.Null(products.Error);
        }
        [Fact]
        public async Task GetProductsReturnAllProductWithValidID()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnAllProducts)).Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            var productprofile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(config);

            var productProvider = new ProductProvider(dbContext, null, mapper);
            var product = await productProvider.getProductAsync(1);


            Assert.True(product.IsSuccess);
            Assert.NotNull(product.product);
            Assert.True(product.product.Id == 1);
            Assert.Null(product.Error);
        }

        [Fact]
        public async Task GetProductsReturnAllProductWithInValidID()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnAllProducts)).Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            var productprofile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(config);

            var productProvider = new ProductProvider(dbContext, null, mapper);
            var product = await productProvider.getProductAsync(-1);


            Assert.False(product.IsSuccess);
            Assert.Null(product.product);
            Assert.NotNull(product.Error);
        }
        private void CreateProducts(ProductDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name=Guid.NewGuid().ToString(),
                    Inventory=i+10,
                    price=(decimal)(i*3.14),
                });
            }
            dbContext.SaveChanges();
        }
    }
}