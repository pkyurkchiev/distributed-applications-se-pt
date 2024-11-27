namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;
    using GustoHub.Data.ViewModels;

    public interface IDishService
    {
        Task<string> AddAsync(POSTDishDto dish);
        Task<bool> ExistsByIdAsync(int dishId);
        Task<Dish> GetByIdAsync(int dishId);
        Task<Dish> GetByNameAsync(string dishName);
        Task<IEnumerable<Dish>> AllAsync();
        Task<string> Remove(int id);
    }
}
