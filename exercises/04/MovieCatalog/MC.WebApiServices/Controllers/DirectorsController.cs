using MC.ApplicationServices;
using MC.ApplicationServices.DTOs;
using System.Web.Http;

namespace MC.WebApiServices.Controllers
{
    public class DirectorsController : ApiController
    {
        private readonly DirectorsManagementService _service = new DirectorsManagementService();

        [HttpGet, Route("api/directors/version")]
        public IHttpActionResult Version()
        {
            return Json("Directors version 1.0");
        }

        [HttpGet, Route("api/directors")]
        public IHttpActionResult Get()
        {
            return Json(_service.GetAll());
        }

        [HttpGet, Route("api/directors/{firstName}/filterbyname")]
        public IHttpActionResult Get(string firstName)
        {
            return Json(_service.GetByFirstName(firstName));
        }

        [HttpPost, Route("api/directors")]
        public IHttpActionResult Post(DirectorDto dto)
        {
            return Json(_service.Save(dto));
        }

        [HttpDelete, Route("api/directors/{id}")]
        public IHttpActionResult Delete(int id)
        {
            return Json(_service.Delete(id));
        }
    }
}
