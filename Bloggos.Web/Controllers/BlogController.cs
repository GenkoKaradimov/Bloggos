using Bloggos.BussinessLogic.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var data = await _blogService.GetMapPageAsync(id);
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

            return View();
        }

        public ActionResult Article(int id)
        {
            return View();
        }
    }
}
