namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.POST;

    public interface ICategoryService
    {
        Task<string> AddAsync(POSTCategoryDto category);
        Task<bool> ExistsByIdAsync(int categoryId);
        Task<GETCategoryDto?> GetByIdAsync(int categoryId);
        Task<GETCategoryDto?> GetByNameAsync(string categoryName);
        Task<IEnumerable<GETCategoryDto>> AllAsync();
        Task<string> Remove(int id);
    }
}
