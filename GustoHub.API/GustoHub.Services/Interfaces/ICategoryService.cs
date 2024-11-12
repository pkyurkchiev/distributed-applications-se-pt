namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;

    public interface ICategoryService
    {
        Task<int> AddCategoryAsync(Category category);
    }
}
