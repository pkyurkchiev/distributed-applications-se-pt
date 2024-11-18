namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;

    public interface IOrderService
    {
        Task<string> AddAsync(Order order);
        Task<bool> ExistsByIdAsync(int orderId);
        Task<Order> GetByIdAsync(int orderId);
        Task<Order> GetByDateAsync(DateTime date);
        Task<IEnumerable<Order>> AllAsync();
        Task<string> Remove(int id);
    }
}
