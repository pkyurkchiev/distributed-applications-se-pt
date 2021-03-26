using MC.Website.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace MC.Website.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly Uri url = new Uri("https://localhost:44348/api/directors");

        // GET: Directors
        public ActionResult Index()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = url;
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = httpClient.GetStringAsync("").Result;

                var directorVMs = JsonConvert.DeserializeObject<IEnumerable<DirectorVM>>(response);

                return View(directorVMs);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DirectorVM directorVM)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = url;
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var stringDirectorVM = JsonConvert.SerializeObject(directorVM);
                var encodingDirectorVM = System.Text.Encoding.UTF8.GetBytes(stringDirectorVM);
                var content = new ByteArrayContent(encodingDirectorVM);
                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                var response = httpClient.PostAsync("", content).Result;
            }

            return RedirectToAction("Index");
        }
    }
}