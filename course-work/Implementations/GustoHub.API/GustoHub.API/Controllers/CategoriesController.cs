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

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var allCategories = await categoryService.AllAsync();
            return Ok(allCategories);
        }

        /// <summary>
        /// Retrieves a category by its name.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <returns>The category if found, otherwise a 404 response.</returns>
        [HttpGet("{categoryName}")]
        public async Task<IActionResult> GetCategoryByName(string categoryName)
        {
            var category = await categoryService.GetByNameAsync(categoryName);

            if (category == null)
            {
                return NotFound("Category not found!");
            }

            return Ok(category);
        }

        /// <summary>
        /// Creates a new category (Admin Only).
        /// </summary>
        /// <param name="categoryDto">The category data to be added.</param>
        /// <returns>A success message.</returns>
        [AuthorizeRole("Admin")]
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] POSTCategoryDto categoryDto)
        {
            string responseMessage = await categoryService.AddAsync(categoryDto);
            return Ok(new { message = responseMessage });
        }

        /// <summary>
        /// Updates an existing category by its ID (Admin Only).
        /// </summary>
        /// <param name="category">The updated category data.</param>
        /// <param name="id">The ID of the category to update.</param>
        /// <returns>A success message or a 404 response if not found.</returns>
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

        /// <summary>
        /// Deletes a category by its ID (Admin Only).
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A success message or a 404 response if not found.</returns>
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
