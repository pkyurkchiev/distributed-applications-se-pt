using MC.ApplicationServices.DTOs;
using MC.ApplicationServices.Implementations;
using System.Web.Http;

namespace MC.WebApiServices.Controllers
{
    public class DirectorsController : BaseController
    {
        #region Properties
        private readonly DirectorManagementService _service = null;
        #endregion

        #region Constructors
        public DirectorsController()
        {
            _service = new DirectorManagementService();
        }
        #endregion

        #region public Methods
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Json(_service.Get());
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            return Json(_service.GetById(id));
        }

        [HttpGet]
        public IHttpActionResult GetByFirstName(string firstName)
        {
            return Json(_service.GetByFirstName(firstName));
        }

        [HttpPost]
        public IHttpActionResult PostDirector(DirectorDto directorDto)
        {
            return Json(_service.Save(directorDto));
        }

        [HttpDelete]
        public IHttpActionResult DeleteDirector(int id)
        {
            return Json(_service.Delete(id));
        }
        #endregion
    }
}
