namespace GustoHub.Services.Services
{
    using GustoHub.Data.Models;
    using GustoHub.Data.Common;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.PUT;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;

        public CategoryService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> AddAsync(POSTCategoryDto categoryDto)
        {
            Category category = new Category() 
            {
                Name = categoryDto.Name,
            };

            await repository.AddAsync(category);
            await repository.SaveChangesAsync();

            return "Category added Successfully!";
        }

        public async Task<IEnumerable<GETCategoryDto>> AllAsync()
        {
            List<GETCategoryDto> categoryDtos = new List<GETCategoryDto>();

            List<Dish> dishes = await repository.AllAsync<Dish>();

            foreach (var category in await repository.AllAsync<Category>())
            {
                List<GETDishDto> dishDtos = dishes
                    .Where(d => d.CategoryId == category.Id) 
                    .Select(d => new GETDishDto
                    {
                        Name = d.Name,
                        Price = d.Price.ToString("F2"), 
                        CategoryId = d.CategoryId
                    })
                    .ToList();

                GETCategoryDto categoryDto = new GETCategoryDto
                {
                    Name = category.Name,
                    DishDtos = dishDtos 
                };

                categoryDtos.Add(categoryDto);
            }

            return categoryDtos;
        }

        public async Task<bool> ExistsByIdAsync(int categoryId)
        {
            return await repository.AllAsReadOnly<Category>().AnyAsync(c => c.Id == categoryId);
        }

        public async Task<GETCategoryDto?> GetByIdAsync(int categoryId)
        {
            List<Dish> dishes = await repository.AllAsync<Dish>();

            List<GETDishDto> dishDtos = dishes
                .Where(d => d.CategoryId == categoryId)
                .Select(d => new GETDishDto
                {
                    Name = d.Name,
                    CategoryId = d.CategoryId,
                    Price = d.Price.ToString("F2")
                })
                .ToList();

            Category? category = await repository.AllAsReadOnly<Category>().FirstOrDefaultAsync(c => c.Id == categoryId);

            GETCategoryDto? categoryDto = new GETCategoryDto() 
            {
                Name = category.Name,
                DishDtos = dishDtos
            };

            return categoryDto;
        }

        public async Task<GETCategoryDto?> GetByNameAsync(string categoryName)
        {
            List<Dish> dishes = await repository.AllAsReadOnly<Dish>()
                .Include(d => d.Category)
                .ToListAsync();

            List<GETDishDto>? dishDtos = dishes
                .Where(d => d.Category.Name == categoryName)
                .Select(d => new GETDishDto
                {
                    Name = d.Name,
                    CategoryId = d.CategoryId,
                    Price = d.Price.ToString("F2")
                })
                .ToList();

            Category? category = await repository.AllAsReadOnly<Category>().FirstOrDefaultAsync(c => c.Name == categoryName);

            GETCategoryDto? categoryDto = new GETCategoryDto()
            {
                Name = category.Name,
                DishDtos = dishDtos
            };

            return categoryDto;
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

        public async Task<string> UpdateAsync(PUTCategoryDto categoryDto, int categoryId)
        {
            Category? category = await repository.GetByIdAsync<Category>(categoryId);          

            category.Name = categoryDto.Name;

            await repository.SaveChangesAsync();

            return "Category updated Successfully!";
        }
    }
}
