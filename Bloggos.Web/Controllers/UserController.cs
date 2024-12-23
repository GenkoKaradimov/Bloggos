using Bloggos.BussinessLogic.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggos.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Settings()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Models.User.LoginViewModel model)
        {
            // Validate input model
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ErrorMessage"] = String.Join(" ", errors);
                return View();
            }

            Bloggos.BussinessLogic.Models.User.UserModel user;

            try
            {
                user = await _userService.LoginUser(new BussinessLogic.Models.User.UserLoginModel()
                {
                    Username = model.Username,
                    Password = model.Password
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0);

            if (user.IsAdmin) return RedirectToAction("Admin", "User");
            else return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(Models.User.RegisterViewModel model)
        {
            // Validate input model
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ErrorMessage"] = String.Join(" ", errors);
                return View();
            }

            Bloggos.BussinessLogic.Models.User.UserModel user;

            try
            {
                user = await _userService.RegisterUser(new BussinessLogic.Models.User.UserRegistrationModel()
                {
                    Username = model.Username,
                    Password = model.Password
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0);

            if (user.IsAdmin) return RedirectToAction("Admin", "User");
            else return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }
    }
}
