namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Infrastructure.Attributes;

    [APIKeyRequired]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
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
            var category
                = await categoryService.GetByNameAsync(categoryName);

            if (category == null) 
            {
                return NotFound("Category not found!");
            }

            return Ok(category);
        }

        [AuthorizeRole("Admin")]
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] POSTCategoryDto categoryDto)
        {
            string responseMessage = await categoryService.AddAsync(categoryDto);
            return Ok(new { message = responseMessage });
        }

        [AuthorizeRole("Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(PUTCategoryDto category, int id)
        {
            if (!await categoryService.ExistsByIdAsync(id))
            {
                return NotFound("Category not found!");
            }

            string responseMessage = await categoryService.UpdateAsync(category, id);

            return Ok(new { message = responseMessage });
        }

        [AuthorizeRole("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            if (!await categoryService.ExistsByIdAsync(id))
            {
                return NotFound("Category not found!");
            }

            string responseMessage = await categoryService.Remove(id);

            return Ok(new { message = responseMessage });
        }
    }
}
