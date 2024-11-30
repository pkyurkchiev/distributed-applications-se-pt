namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.POST;

    public interface IOrderService
    {
        Task<string> AddAsync(POSTOrderDto orderDto);
        Task<bool> ExistsByIdAsync(int orderId);
        Task<GETOrdersDto> GetByIdAsync(int orderId);
        Task<GETOrdersDto> GetByDateAsync(DateTime date);
        Task<IEnumerable<GETOrdersDto>> AllAsync();
        Task<string> Remove(int id);
    }
}
