namespace GustoHub.Services.Services
{
    using GustoHub.Data.Common;
    using GustoHub.Data.Models;
    using GustoHub.Services.Interfaces;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;

        public CategoryService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            await repository.AddAsync(category);
            await repository.SaveChangesAsync();

            return category.Id;
        }
    }
}
