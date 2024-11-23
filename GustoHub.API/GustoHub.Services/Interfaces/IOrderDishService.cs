namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;

    public interface IOrderDishService
    {
        public Task AddDishToOrder(int orderId, int dishId, int quantity);
        public Task<IEnumerable<Dish>> GetDishesForOrder(int orderId);
    }
}
