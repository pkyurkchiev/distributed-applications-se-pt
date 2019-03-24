using System.Web.Mvc;
using System.Net.Http;
using MC.Website.GenreReference;
using System.Collections.Generic;
using MC.Website.ViewModels;
using System.Linq;

namespace MC.Website.Controllers
{
    public class GenersController : Controller
    {
        // GET: Geners
        public ActionResult Index()
        {
            using (GenreClient client = new GenreClient())
            {
                List<GenreVM> genreVMs = client.GetGenres().Select(x => new GenreVM(x)).ToList();

                return View(genreVMs);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GenreVM genreVM)
        {
            using (GenreClient client = new GenreClient())
            {
                client.PostGenre(new GenreDto
                {
                    Name = genreVM.Name,
                    IsActive = true
                });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (GenreClient client = new GenreClient())
            {                
                return View(new GenreVM(client.GetGenreById(id)));
            } 
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            using (GenreClient client = new GenreClient())
            {
                client.DeleteGenre(id);
            }
            return RedirectToAction("Index");
        }
    }
}