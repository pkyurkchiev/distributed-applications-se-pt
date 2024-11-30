namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.POST;

    public interface IDishService
    {
        Task<string> AddAsync(POSTDishDto dish);
        Task<bool> ExistsByIdAsync(int dishId);
        Task<GETDishDto> GetByIdAsync(int dishId);
        Task<GETDishDto> GetByNameAsync(string dishName);
        Task<IEnumerable<GETDishDto>> AllAsync();
        Task<string> Remove(int id);
    }
}
