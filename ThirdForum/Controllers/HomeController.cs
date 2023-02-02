using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using ThirdForum.Data;
using ThirdForum.Models;

namespace ThirdForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContext_f _db;
        public const string SessionKeyName = "_Name";

        public HomeController(ILogger<HomeController> logger, DbContext_f bd)
        {
            _logger = logger;
            _db = bd;
        }
        [Route("/forum/{id}")]
        public IActionResult Index(int id)
        {
            ViewData["Message"] = HttpContext.Session.GetString(SessionKeyName);

            var theme = new HomeModel
            {
                user = _db.User.Find(id),
               themes = _db.Theme.Include(x => x.Sujets)
        };
            return View(theme);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}