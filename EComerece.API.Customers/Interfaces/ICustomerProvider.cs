using EComerece.API.Customers.DB;

namespace EComerece.API.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Customer>? Customers, string? ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Models.Customer? Customer, string? ErrorMessage)> GetCustomerAsync(int id);
    }
}
