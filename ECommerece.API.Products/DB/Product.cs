namespace ECommerece.Api.Products.DB
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal price { get; set; }
        public int Inventory { get; set; }
    }
}
