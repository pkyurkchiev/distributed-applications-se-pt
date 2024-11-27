namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;
    using GustoHub.Data.ViewModels;

    public interface ICustomerService
    {
        Task<string> AddAsync(POSTCustomerDto customer);
        Task<bool> ExistsByIdAsync(Guid customerId);
        Task<Customer> GetByIdAsync(Guid customerId);
        Task<Customer> GetByNameAsync(string customerName);
        Task<IEnumerable<Customer>> AllAsync();
        Task<string> Remove(Guid customerId);
    }
}
