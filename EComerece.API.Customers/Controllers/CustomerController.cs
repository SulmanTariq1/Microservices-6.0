using EComerece.API.Customers.Interfaces;
using EComerece.API.Customers.Providers;
using Microsoft.AspNetCore.Mvc;

namespace EComerece.API.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController:ControllerBase
    {
        private readonly ICustomerProvider customersProvider;

        public CustomerController(ICustomerProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }
    }
}
