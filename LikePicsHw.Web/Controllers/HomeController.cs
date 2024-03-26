using LikePicsHw.Data;
using LikePicsHw.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LikePicsHw.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connection;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _connection = configuration.GetConnectionString("ConStr");
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var repo = new Repository(_connection);
            return View(new IndexViewModel { Pics=repo.GetAllPics()});
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile imageFile, string name)
        {
            var fileName = $"{Guid.NewGuid()}-{imageFile.FileName}";
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

            using FileStream fs = new(fullPath, FileMode.Create);
            imageFile.CopyTo(fs);

            var repo = new Repository(_connection);
            repo.Upload(new()
            {
                ImagePath=fileName,
                Name=name,
                LikesCount=0,
                DateTime=DateTime.Now
            });

            return Redirect("/");
        }

        public IActionResult ViewImage(int id)
        {
            var repo = new Repository(_connection);
            var vm = new ViewImageViewModel { Image = repo.GetImageById(id) };
            if (vm.Image == null)
            {
                return Redirect("/");
            }
            return View(vm);
        }

        [HttpPost]
        public void Like(int id)
        {
            var repo = new Repository(_connection);
            repo.AddLike(id);
        }

        public IActionResult GetLikeCount(int id)
        {
            var repo = new Repository(_connection);
            return Json(repo.GetImageById(id));
        }
    }
}