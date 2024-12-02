namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;

    public interface IOrderDishService
    {
        public Task<string> AddDishToOrder(POSTOrderDishDto orderDishDto);
        public Task<IEnumerable<Dish>> GetDishesForOrder(int orderId);
        public Task<GETOrderDishDto?> GetOrderDishByIdAsync(int orderId, int dishId);
    }
}
