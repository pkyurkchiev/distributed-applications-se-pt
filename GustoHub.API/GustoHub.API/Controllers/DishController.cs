namespace GustoHub.API.Controllers
{
    using GustoHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;

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

        [HttpPost]
        public async Task<IActionResult> PostDish([FromBody] POSTDishDto dishDto)
        {
            if (!await categoryService.ExistsByIdAsync(dishDto.CategoryId))
            {
                return NotFound("Category not found!");
            }
            string responseMessage = await dishService.AddAsync(dishDto);
            return Ok(responseMessage);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllDishs()
        {
            var allDishes = await dishService.AllAsync();
            return Ok(allDishes);
        }
        [HttpGet("{dishName}")]
        public async Task<IActionResult> GetDishByName(string dishName)
        {
            var dish = await dishService.GetByNameAsync(dishName);
            return Ok(dishName);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDish(int id)
        {
            return Ok(await dishService.Remove(id));
        }
    }
}
