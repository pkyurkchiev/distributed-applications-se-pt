namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] POSTCategoryDto categoryDto)
        {
            string responseMessage = await categoryService.AddAsync(categoryDto);
            return Ok(responseMessage);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var allCategories = await categoryService.AllAsync();
            return Ok(allCategories);
        }
        [HttpGet("{categoryName}")]
        public async Task<IActionResult> GetCategoryByName(string categoryName)
        {
            var category = await categoryService.GetByNameAsync(categoryName);
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            return Ok(await categoryService.Remove(id));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(PUTCategoryDto category, int id)
        {
            return Ok(await categoryService.UpdateAsync(category, id));
        }
    }
}
