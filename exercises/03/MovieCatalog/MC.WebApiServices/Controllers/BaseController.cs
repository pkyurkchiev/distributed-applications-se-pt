using System.Web.Http;

namespace MC.WebApiServices.Controllers
{
    public class BaseController : ApiController
    {
        [HttpGet]
        public string Version()
        {
            return "Web Api version 1.0.0";
        }
    }
}
