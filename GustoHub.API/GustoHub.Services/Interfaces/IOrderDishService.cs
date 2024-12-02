namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;

    public interface IOrderDishService
    {
        Task<string> AddDishToOrder(POSTOrderDishDto orderDishDto);
        Task<IEnumerable<Dish>> GetDishesForOrder(int orderId);
        Task<GETOrderDishDto?> GetOrderDishByIdAsync(int orderId, int dishId);
        
    }
}
