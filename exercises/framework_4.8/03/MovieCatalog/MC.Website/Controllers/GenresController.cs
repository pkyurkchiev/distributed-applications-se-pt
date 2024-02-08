using MC.Website.GenresServiceReference;
using MC.Website.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MC.Website.Controllers
{
    public class GenresController : Controller
    {

        // GET: Genres
        public ActionResult Index()
        {
            using (GenresClient client = new GenresClient())
            {
                List<GenreVM> genreVMs = client.GetGenres().Select(x => new GenreVM(x.Id, x.Name)).ToList();

                return View(genreVMs);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GenreVM genreVM)
        {
            using (GenresClient client = new GenresClient())
            {
                client.Save(genreVM.Name);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (GenresClient client = new GenresClient())
            {
                var genreDto = client.GetById(id);
                return View(new GenreVM(genreDto.Id, genreDto.Name));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            using (GenresClient client = new GenresClient())
            {
                client.Delete(id);
                return RedirectToAction("Index");
            }
        }
    }
}