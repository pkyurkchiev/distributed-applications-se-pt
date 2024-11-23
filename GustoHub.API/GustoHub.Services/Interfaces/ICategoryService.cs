namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;

    public interface ICategoryService
    {
        Task<string> AddAsync(Category category);
        Task<bool> ExistsByIdAsync(int categoryId);
        Task<Category> GetByIdAsync(int categoryId);
        Task<Category> GetByNameAsync(string categoryName);
        Task<IEnumerable<Category>> AllAsync();
        Task<string> Remove(int id);
    }
}
