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
                model.DescriptionHTML = data.DescriptionHTML;

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
        public async Task<ActionResult> Articles()
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

        #region Administration of Maps and MapLinks

        [HttpGet]
        public async Task<ActionResult> Maps()
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            MapsViewModel model = new MapsViewModel();

            try
            {
                var data = await _blogService.GetMapPagesAsync();

                model.Maps = data.MapPages.Select(x => new MapViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    DescriptionHTML = x.DescriptionHTML,
                    Destinations = x.Destinations.Select(d => new MapLinkViewModel()
                    {
                        Title = d.Title,
                        Description = d.Description,
                        ImageSource = d.ImageSource,
                        DestinationId = d.DestinationId,
                        DestinationType = (LinkDestinationViewModel)(int)d.DestinationType
                    })
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
        public ActionResult CreateMap()
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateMap(MapViewModel model)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            try
            {
                await _blogService.AddMapPageAsync(new BussinessLogic.Models.Blog.MapPageModel()
                {
                    Title = model.Title,
                    DescriptionHTML = model.DescriptionHTML
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Maps", "Blog");
            }

            return RedirectToAction("Maps", "Blog");
        }

        [HttpGet]
        public async Task<ActionResult> EditMap(int id)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            var model = new MapViewModel();

            try
            {
                var data = await _blogService.GetMapPageAsync(id);

                model.Id = data.Id;
                model.Title = data.Title;
                model.DescriptionHTML = data.DescriptionHTML;

                model.Destinations = new List<MapLinkViewModel>();
                model.Destinations = data.Destinations.Select(x => new MapLinkViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    DestinationId = x.DestinationId,
                    ImageSource = x.ImageSource,
                    DestinationType = (LinkDestinationViewModel)(int)x.DestinationType,
                    OrderWeight = x.OrderWeight
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Maps", "Blog");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditMap(MapViewModel model)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            try
            {
                await _blogService.EditMapPageAsync(new BussinessLogic.Models.Blog.MapPageModel()
                {
                    Id = model.Id,
                    Title = model.Title,
                    DescriptionHTML = model.DescriptionHTML
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Maps", "Blog");
            }

            return RedirectToAction("Maps", "Blog");
        }

        [HttpGet]
        public async Task<ActionResult> AddMapLink(int mapId)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            var model = new MapLinkViewModel();
            model.MapId = mapId;
            model.DestinationOptions = new Dictionary<int, string>()
            {
                {0, "ArticlePage" },
                {1, "MapPage" }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddMapLink(MapLinkViewModel model)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            try
            {
                await _blogService.AddMapLinkAsync(new BussinessLogic.Models.Blog.LinkModel()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    DestinationId = model.DestinationId,
                    DestinationType = (BussinessLogic.Models.Blog.LinkDestination)(int)model.DestinationType,
                    ImageSource = model.ImageSource,
                    MapId = model.MapId,
                    OrderWeight = model.OrderWeight
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Maps", "Blog");
            }

            return RedirectToAction("Maps", "Blog");
        }


        [HttpGet]
        public async Task<ActionResult> EditMapLink(int id)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            var model = new MapLinkViewModel();

            model.DestinationOptions = new Dictionary<int, string>()
            {
                {0, "ArticlePage" },
                {1, "MapPage" }
            };

            try
            {
                var data = await _blogService.GetLinkModel(id);

                model.Id = data.Id;
                model.Title = data.Title;
                model.Description = data.Description;
                model.ImageSource = data.ImageSource;
                model.DestinationId = data.DestinationId;
                model.DestinationType = (Models.Blog.LinkDestinationViewModel)(int)data.DestinationType;
                model.MapId = data.MapId;
                model.OrderWeight = data.OrderWeight;

            } catch (Exception ex) {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Maps", "Blog");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditMapLink(MapLinkViewModel model)
        {
            // is user administrator
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

            try
            {
                await _blogService.EditMapLinkAsync(new BussinessLogic.Models.Blog.LinkModel()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    DestinationId = model.DestinationId,
                    DestinationType = (BussinessLogic.Models.Blog.LinkDestination)(int)model.DestinationType,
                    ImageSource = model.ImageSource,
                    MapId = model.MapId,
                    OrderWeight = model.OrderWeight
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("EditMap", "Blog", model.MapId);
            }

            return RedirectToAction("Maps", "Blog");
        }

        #endregion
    }
}
