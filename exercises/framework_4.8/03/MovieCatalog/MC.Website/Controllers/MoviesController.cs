using MC.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MC.Website.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoviesServiceReference.MoviesClient moviesClient = new MoviesServiceReference.MoviesClient();

        // GET: Movies
        public ActionResult Index()
        {
            var movies = moviesClient.GetMovies().Select(x => new MovieVM(x.Id, x.Title, x.Country, x.Genre.Name, x.Director.FirstName + " " + x.Director.LastName));
            return View(movies);
        }
    }
}