namespace GustoHub.Services.Services
{
    using GustoHub.Data.Common;
    using GustoHub.Data.Models;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DishService : IDishService
    {
        private readonly IRepository repository;

        public DishService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> AddAsync(POSTDishDto dishDto)
        {
            Dish dish = new Dish()
            {
                Name = dishDto.Name,
                CategoryId = dishDto.CategoryId,
                Price = dishDto.Price,
            };

            await repository.AddAsync(dish);
            await repository.SaveChangesAsync();

            return "Dish added Successfully!";
        }

        public async Task<IEnumerable<GETDishDto>> AllAsync()
        {
            List<GETDishDto> dishes = await repository.AllAsReadOnly<Dish>()
                .Select(d => new GETDishDto()
                {
                    Name = d.Name,
                    Price = d.Price.ToString("F2"),
                    CategoryId = d.CategoryId,
                })
                .ToListAsync();

            return dishes;
        }

        public async Task<bool> ExistsByIdAsync(int dishId)
        {
            return await repository.AllAsReadOnly<Dish>().AnyAsync(d => d.Id == dishId);
        }

        public async Task<GETDishDto> GetByIdAsync(int dishId)
        {
            Dish dish = await repository.AllAsReadOnly<Dish>().FirstOrDefaultAsync(d => d.Id == dishId);

            GETDishDto dishDto = new GETDishDto()
            {
                Name = dish.Name,
                Price = dish.Price.ToString("F2"),
                CategoryId = dish.CategoryId,
            };

            return dishDto;
        }

        public async Task<GETDishDto> GetByNameAsync(string dishName)
        {
            Dish dish = await repository.AllAsReadOnly<Dish>().FirstOrDefaultAsync(d => d.Name == dishName);

            GETDishDto dishDto = new GETDishDto()
            {
                Name = dish.Name,
                Price = dish.Price.ToString("F2"),
                CategoryId = dish.CategoryId,
            };

            return dishDto;
        }

        public async Task<string> Remove(int id)
        {
            if (await ExistsByIdAsync(id))
            {
                await repository.RemoveAsync<Dish>(id);
                await repository.SaveChangesAsync();
                return "Dish removed successfully!";
            }
            return "Dish doesn't exists!";
        }

        public async Task<string> UpdateAsync(PUTDishDto dishDto, int dishId)
        {
            Dish? dish  = await repository.AllAsReadOnly<Dish>()
                .FirstOrDefaultAsync(d => d.Id == dishId);

            dish.Name = dishDto.Name;
            dish.Price = dishDto.Price;
            dish.CategoryId = dishDto.CategoryId;

            await repository.SaveChangesAsync();

            return dish.Name;
        }
    }
}
