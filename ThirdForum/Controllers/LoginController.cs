using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ThirdForum.Data;

namespace ThirdForum.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContext_f _db;
        public const string SessionKeyName = "_user";

        public LoginController(ILogger<HomeController> logger, DbContext_f bd)
        {
            _logger = logger;
            _db = bd;
        }

        [Route("/")]
        public IActionResult login()
        {

            return View();
        }

        public IActionResult loginSubmit()
        {
            var username = Request.Form["login"];
            var password =Request.Form["password"];
            //return Json(password);
            var az= Json(new { username, password });
            //return az
            var result = _db.User.FirstOrDefault(x => x.psuedo == username.ToString() && x.motpasse == password.ToString());
            if (result == null)
            {
                return Redirect("~/");

            }
            else if(result.role=="user")
            {
                 HttpContext.Session.SetString(SessionKeyName, result.ToString());
                return Redirect("~/forum/"+result.Id);
            }
            else
            {
                return Redirect("~/admin");

            }
        }
    }
}
