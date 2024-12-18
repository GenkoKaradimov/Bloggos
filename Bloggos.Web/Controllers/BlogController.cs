using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggos.Web.Controllers
{
    public class BlogController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map(int id)
        {
            return View();
        }

        public ActionResult Article(int id)
        {
            return View();
        }
    }
}
