namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;

    public interface IDishService
    {
        Task<string> AddAsync(Dish dish);
        Task<bool> ExistsByIdAsync(int dishId);
        Task<Dish> GetByIdAsync(int dishId);
        Task<Dish> GetByNameAsync(string dishName);
        Task<IEnumerable<Dish>> AllAsync();
        Task<string> Remove(int id);
    }
}
