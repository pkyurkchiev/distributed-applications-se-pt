namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;

    public interface IDishService
    {
        Task<string> AddAsync(POSTDishDto dish);
        Task<bool> ExistsByIdAsync(int dishId);
        Task<GETDishDto> GetByIdAsync(int dishId);
        Task<GETDishDto?> GetByNameAsync(string dishName);
        Task<IEnumerable<GETDishDto>> AllAsync();
        Task<string> Remove(int id);
        Task<string> UpdateAsync(PUTDishDto dish, int dishId);
    }
}
