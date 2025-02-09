namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Infrastructure.Attributes;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService dishService;
        private readonly ICategoryService categoryService;

        public DishController(
            IDishService dishService,
            ICategoryService categoryService)
        {
            this.dishService = dishService;
            this.categoryService = categoryService;
        }

        /// <summary>
        /// Retrieves all dishes.
        /// </summary>
        /// <returns>A list of all available dishes.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllDishes()
        {
            var allDishes = await dishService.AllAsync();
            return Ok(allDishes);
        }

        /// <summary>
        /// Retrieves a dish by its name.
        /// </summary>
        /// <param name="dishName">The name of the dish.</param>
        /// <returns>The dish if found, otherwise a 404 response.</returns>
        [HttpGet("{dishName}")]
        public async Task<IActionResult> GetDishByName(string dishName)
        {
            var dish = await dishService.GetByNameAsync(dishName);

            if (dish == null)
            {
                return NotFound(new { message = "Dish not found!" });
            }

            return Ok(dish);
        }

        /// <summary>
        /// Creates a new dish (Admin Only, API Key Required).
        /// </summary>
        /// <param name="dishDto">The dish data to be added.</param>
        /// <returns>A success message or a 404 response if the category is not found.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPost]
        public async Task<IActionResult> PostDish([FromBody] POSTDishDto dishDto)
        {
            if (!await categoryService.ExistsByIdAsync(dishDto.CategoryId))
            {
                return NotFound(new { message = "Category not found!" });
            }

            string responseMessage = await dishService.AddAsync(dishDto);
            return Ok(new { message = responseMessage });
        }

        /// <summary>
        /// Updates an existing dish by its ID (Admin Only, API Key Required).
        /// </summary>
        /// <param name="dish">The updated dish data.</param>
        /// <param name="id">The ID of the dish to update.</param>
        /// <returns>A success message or a 404 response if the dish is not found.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDish(PUTDishDto dish, int id)
        {
            if (!await dishService.ExistsByIdAsync(id))
            {
                return NotFound(new { message = "Dish not found!" });
            }

            string responseMessage = await dishService.UpdateAsync(dish, id);
            return Ok(new { message = responseMessage });
        }

        /// <summary>
        /// Deletes a dish by its ID (Admin Only, API Key Required).
        /// </summary>
        /// <param name="id">The ID of the dish to delete.</param>
        /// <returns>A success message or a 404 response if the dish is not found.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDish(int id)
        {
            if (!await dishService.ExistsByIdAsync(id))
            {
                return NotFound(new { message = "Dish not found!" });
            }

            string responseMessage = await dishService.Remove(id);
            return Ok(new { message = responseMessage });
        }
    }
}
