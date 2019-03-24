using MC.Website.MovieReference;
using MC.Website.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MC.Website.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            using (MoviesClient client = new MoviesClient())
            {
                List<MovieVM> movieVMs = client.GetMovies().Select(x => new MovieVM(x)).ToList();

                return View(movieVMs);
            };
        }
    }
}