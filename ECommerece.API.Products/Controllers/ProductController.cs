using ECommerece.Api.Products.DB.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerece.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController: ControllerBase
    {
        public IProductProvider ProductProvider { get; }
        public ProductController(IProductProvider productProvider)
        {
            ProductProvider = productProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result= await ProductProvider.getProductsAsync();
            if(result.IsSuccess)
            {
                return Ok(result.products);
            }
            return NotFound();

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await ProductProvider.getProductAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.product);
            }
            return NotFound();

        }
    }
}
