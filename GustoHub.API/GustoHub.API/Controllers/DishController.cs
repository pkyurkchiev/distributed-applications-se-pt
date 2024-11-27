namespace GustoHub.API.Controllers
{
    using GustoHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService dishService;

        public DishController(IDishService dishService)
        {
            this.dishService = dishService;
        }

        [HttpPost]
        public async Task<IActionResult> PostDish([FromBody] Dish dish)
        {
            string responseMessage = await dishService.AddAsync(dish);
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
