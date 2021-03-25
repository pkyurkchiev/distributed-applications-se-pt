using MC.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MC.Website.Controllers
{
    public class GenresController : Controller
    {
        private readonly GenresServiceReference.GenresClient genresClient = new GenresServiceReference.GenresClient();

        // GET: Genres
        public ActionResult Index()
        {
            var genres = genresClient.GetGenres().Select(x => new GenreVM(x.Id, x.Name));
            return View(genres);
        }
    }
}