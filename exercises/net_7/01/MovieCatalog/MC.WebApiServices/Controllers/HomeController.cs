using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MC.WebApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello world";
        }

        [HttpGet("hello")]
        public string Get([FromQuery] int count)
        {
            StringBuilder sb = new();

            for (int i = 0; i < count; i++)
            {
                sb.AppendLine("Hello world!");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Method that return Hello list.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="lenght">For lenght.</param>
        /// <returns>List with specific count of hellos.</returns>
        [HttpGet("names/{name}")]
        public string Get([FromRoute] string name, [FromQuery] int lenght)
        {
            StringBuilder sb = new();
            for (int i = 0; i < lenght; i++)
            {
                sb.AppendLine($"Hello {name}!");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calculate Perimeter and Area.
        /// </summary>
        /// <param name="a">Rectangle a.</param>
        /// <param name="b">Rectangle b.</param>
        /// <returns>Return Shape object.</returns>
        [HttpGet("calculator")]
        [ProducesResponseType(typeof(Shape), StatusCodes.Status200OK)]
        public IActionResult Calculator([FromQuery] double a, [FromQuery] double b)
        {
            return Ok(new Shape(a, b));
        }
    }

    /// <summary>
    /// Shape data object.
    /// </summary>
    public class Shape
    {
        /// <summary>
        /// Gets or sets perimeter of rectangle.
        /// </summary>
        public string Perimeter { get; set; }

        /// <summary>
        /// Gets or sets area of rectangle.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        /// <param name="a">Rectangle a.</param>
        /// <param name="b">Rectangle b.</param>
        public Shape(double a, double b) 
        {
            Perimeter = $"Perimeter = { 2*a + 2*b }";
            Area = $"Area = { a * b }";
        }
    }
}
