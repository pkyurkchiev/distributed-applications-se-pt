using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging;
using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MC.WebApiServices.Controllers
{
    /// <summary>
    /// Movies controller.
    /// </summary>
    [Authorize]
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
        /// Get movie list.
        /// </summary>
        /// <param name="isActive">Active or UnActive fitler.</param>
        /// <returns>Retrun list fitler by active filter.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetMovieResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovie([FromQuery] bool isActive) => Ok(await _service.GetMovieAsync(new(isActive)));

        /// <summary>
        /// Get movies by title.
        /// </summary>
        /// <param name="title">Movie title.</param>
        /// <returns>Single movie filter by title</returns>
        [HttpGet("search/{title}")]
        [ProducesResponseType(typeof(GetByTitleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] string title) => Ok(await _service.GetByTitleAsync(new(title)));

        /// <summary>
        /// Save movie.
        /// </summary>
        /// <returns>Return null if not success.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateMovieResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMovie([FromBody] MovieModel model) => Ok(await _service.SaveAsync(new(model)));
    }
}
