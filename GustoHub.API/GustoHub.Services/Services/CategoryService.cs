namespace GustoHub.Services.Services
{
    using GustoHub.Data.Models;
    using GustoHub.Data.Common;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;

        public CategoryService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> AddAsync(Category category)
        {
            if (!await ExistsByIdAsync(category.Id))
            {
                await repository.AddAsync(category);
                await repository.SaveChangesAsync();

                return "Category added Successfully!";
            }

            return "Category already exists!";
        }

        public async Task<IEnumerable<Category>> AllAsync()
        {
            return await repository.AllAsync<Category>();
        }

        public async Task<bool> ExistsByIdAsync(int categoryId)
        {
            return await repository.AllAsReadOnly<Category>().AnyAsync(c => c.Id == categoryId);
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            return await repository.AllAsReadOnly<Category>().FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<Category> GetByNameAsync(string categoryName)
        {
            return await repository.AllAsReadOnly<Category>().FirstOrDefaultAsync(c => c.Name == categoryName);
        }

        public async Task<string> Remove(int id)
        {
            if (await ExistsByIdAsync(id))
            {
                await repository.RemoveAsync<Category>(id);
                await repository.SaveChangesAsync();
                return "Category removed successfully!";
            }
            return "Category doesn't exists!";
        }
    }
}
