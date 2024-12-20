using Bloggos.BussinessLogic.IServices;
using Bloggos.Web.Models.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bloggos.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Map(int id)
        {
            var model = new MapViewModel();

            try
            {
                var data = await _blogService.GetMapPageAsync(id);

                model.Id = data.Id;
                model.Title = data.Title;
                model.Description = data.Description;

                model.Destinations = new List<MapLinkViewModel>();
                model.Destinations = data.Destinations.Select(x => new MapLinkViewModel()
                {
                    Title = x.Title,
                    Description = x.Description,
                    DestinationId = x.DestinationId,
                    ImageSource = x.ImageSource,
                    DestinationType = (LinkDestinationViewModel)(int)x.DestinationType
                });
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<ActionResult> Article(int id)
        {
            var model = new ArticleViewModel();

            try
            {
                var data = await _blogService.GetArticleAsync(id);

                model.Id = data.Id;
                model.Title = data.Title;
                model.HtmlContent = data.HtmlContent;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
