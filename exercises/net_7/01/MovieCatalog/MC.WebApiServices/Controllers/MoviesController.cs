using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MC.WebApiServices.Controllers
{
    /// <summary>
    /// Movies controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _service;
        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesController"/> class.
        /// </summary>
        /// <param name="service">Movie service.</param>
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get movies by title.
        /// </summary>
        /// <param name="title">Movie title.</param>
        /// <returns>Single movie filter by title</returns>
        [HttpGet("search/{title}")]
        [ProducesResponseType(typeof(GetByTitleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromRoute] string title) => Ok(await _service.GetByTitleAsync(new(title)));
    }
}
