using Bloggos.BussinessLogic.IServices;
using Bloggos.BussinessLogic.Models.Home;
using Bloggos.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggos.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Images(int? count, int? page)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            var resp = new Models.Home.ImagesViewModel();

            try
            {
                var r = await _homeService.GetImagesAsync(count, page);
                resp.ImagesIds = r.ImagesIds;
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Home");
            }

            return View(resp);
        }

        public async Task<IActionResult> Image(int id)
        {
            BussinessLogic.Models.Home.ImageModel image;

            try
            {
                image = await _homeService.GetImageAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return File(image.ImageData, image.ContentType);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(Models.Home.ImageViewModel model)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            try
            {
                var x = new ImageModel()
                {
                    ContentType = model.image.ContentType
                };

                using (var memoryStream = new MemoryStream())
                {
                    await model.image.CopyToAsync(memoryStream);
                    x.ImageData = memoryStream.ToArray();
                }

                await _homeService.AddImageAsync(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Images");
        }

        [HttpPost]
        public async Task<IActionResult> EditImage(Models.Home.ImageViewModel model)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            try
            {
                var x = new ImageModel()
                {
                    Id = model.Id,
                    ContentType = model.image.ContentType
                };

                using (var memoryStream = new MemoryStream())
                {
                    await model.image.CopyToAsync(memoryStream);
                    x.ImageData = memoryStream.ToArray();
                }

                await _homeService.EditImageAsync(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Images");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
