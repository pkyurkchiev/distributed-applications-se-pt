using MC.Website.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace MC.Website.Controllers
{
    public class GenresController : Controller
    {
        // GET: Genres
        public ActionResult Index()
        {
            using (GenreReference.GenreClient client = new GenreReference.GenreClient())
            {

                List<GenreVM> genreVMs = client.GetGenres().Select(x => new GenreVM(x)).ToList();

                return View(genreVMs);
            }
        }
    }
}