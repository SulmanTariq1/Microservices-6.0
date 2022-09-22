namespace ECommerece.Api.Products.DB.Interfaces
{
    public interface IProductProvider
    {
        public Task<(bool IsSuccess, IEnumerable<Models.Product>? products, string? Error)> getProductsAsync();
        public Task<(bool IsSuccess, Models.Product? product, string? Error)> getProductAsync(int id);
    }
}
