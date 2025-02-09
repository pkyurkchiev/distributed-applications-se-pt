using MC.ApplicationServices;
using MC.ApplicationServices.DTOs;
using System.Web.Http;

namespace MC.WebApiServices.Controllers
{
    /// <summary>
    /// Provide Director CRUD endpoints
    /// </summary>
    public class DirectorsController : ApiController
    {
        private readonly DirectorsManagementService _service = new DirectorsManagementService();

        /// <summary>
        /// This methods return version of Director controller
        /// </summary>
        /// <returns>version string</returns>
        [HttpGet, Route("api/directors/version")]
        public IHttpActionResult Version()
        {
            return Json("Directors version 1.0");
        }

        /// <summary>
        /// Get all unfilter Directors
        /// </summary>
        /// <returns>director list</returns>
        [HttpGet, Route("api/directors")]
        public IHttpActionResult Get()
        {
            return Json(_service.GetAll());
        }

        /// <summary>
        /// Get Director by unique id
        /// </summary>
        /// <param name="id">director id</param>
        /// <returns></returns>
        [HttpGet, Route("api/directors/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Json(_service.GetById(id));
        }

        /// <summary>
        /// Get Director by first name
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <returns></returns>
        [HttpGet, Route("api/directors/{firstName}/filterbyname")]
        public IHttpActionResult Get(string firstName)
        {
            return Json(_service.GetByFirstName(firstName));
        }

        /// <summary>
        /// Create Director
        /// </summary>
        /// <param name="dto">dicrector names</param>
        /// <returns></returns>
        [HttpPost, Route("api/directors")]
        public IHttpActionResult Post(DirectorDto dto)
        {
            return Json(_service.Save(dto));
        }

        /// <summary>
        /// Delete Director
        /// </summary>
        /// <param name="id">unique id</param>
        /// <returns></returns>
        [HttpDelete, Route("api/directors/{id}")]
        public IHttpActionResult Delete(int id)
        {
            return Json(_service.Delete(id));
        }
    }
}
