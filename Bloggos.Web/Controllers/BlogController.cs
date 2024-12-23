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

        #region Adminstration of articles

        [HttpGet]
        public async Task<ActionResult> Articles(int id)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1) 
                return RedirectToAction("Login", "User");

            ArticlesViewModel model = new ArticlesViewModel();

            try
            {
                var data = await _blogService.AllArticles();

                model.Articles = data.Select(x => new ArticleViewModel { 
                    Id = x.Id,
                    Title = x.Title,
                    HtmlContent = x.HtmlContent
                }).ToList();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateArticle()
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateArticle(ArticleViewModel model)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            try
            {
                await _blogService.AddArticleAsync(new BussinessLogic.Models.Blog.ArticleModel()
                {
                    Title = model.Title,
                    HtmlContent= model.HtmlContent
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Articles", "Blog");
            }

            return RedirectToAction("Articles", "Blog");
        }

        [HttpGet]
        public async Task<ActionResult> EditArticle(int id)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User"); 
            
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
                return RedirectToAction("Articles", "Blog");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditArticle(ArticleViewModel model)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            try
            {
                await _blogService.EditArticleAsync(new BussinessLogic.Models.Blog.ArticleModel()
                {
                    Id = model.Id,
                    Title = model.Title,
                    HtmlContent = model.HtmlContent
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Articles", "Blog");
            }

            return RedirectToAction("Articles", "Blog");
        }

        #endregion
    }
}
