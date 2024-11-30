namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;

    public interface ICustomerService
    {
        Task<string> AddAsync(POSTCustomerDto customer);
        Task<bool> ExistsByIdAsync(Guid customerId);
        Task<GETCustomerDto> GetByIdAsync(Guid customerId);
        Task<GETCustomerDto> GetByNameAsync(string customerName);
        Task<IEnumerable<GETCustomerDto>> AllAsync();
        Task<string> Remove(Guid customerId);
        Task<string> UpdateAsync(PUTCustomerDto customer, string customerId);
    }
}
