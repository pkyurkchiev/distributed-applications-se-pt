using MC.Website.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MC.Website.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            var movieVMs = new List<MovieVM>();

            using (MovieReference.MoviesClient service = new MovieReference.MoviesClient())
            {
                foreach (var item in service.GetMovies())
                {
                    movieVMs.Add(new MovieVM(item));
                }
            }

            return View(movieVMs);
        }
    }
}