namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;

    public interface ICustomerService
    {
        Task<string> AddAsync(Customer customer);
        Task<bool> ExistsByIdAsync(Guid customerId);
        Task<Customer> GetByIdAsync(Guid customerId);
        Task<Customer> GetByNameAsync(string customerName);
        Task<IEnumerable<Customer>> AllAsync();
        Task<string> Remove(Guid customerId);
    }
}
