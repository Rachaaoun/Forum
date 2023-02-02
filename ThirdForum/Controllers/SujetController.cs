using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Buffers;
using System.Diagnostics;
using System.Text;
using ThirdForum.Data;
using ThirdForum.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ThirdForum.Controllers
{
    public class SujetController : Controller
    {
        // GET: SujetController
        private readonly ILogger<HomeController> _logger;
        private readonly DbContext_f _db;
        public const string SessionKeyName = "_Name";
        public SujetController(ILogger<HomeController> logger, DbContext_f bd)
        {
            _logger = logger;
            _db = bd;
        }

       
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: SujetController/updateMes/5/
        public ActionResult updateMes(int id, int person)
        {
            var name = HttpContext.Session.GetString(SessionKeyName);
            var messageSujetUcsers = _db.MessageSujetUser.Where(b => b.Sujets.Id == id && b.User.Id == person).Include(e => e.Message).OrderByDescending(e => e.Message.Id).Include(z => z.User).Include(f=>f.Sujets); 
            var model = new UserUpdate
            {
                messageSujetUser = _db.MessageSujetUser.Where(b => b.Sujets.Id == id && b.User.Id == person).Include(e => e.Message).OrderByDescending(e => e.Message.Id).Include(z => z.User).Include(f => f.Sujets).ToList(),
            };
            return View(model);
        }
        // Post: SujetController/updateMessagee
        [HttpPost]
            public ActionResult updateMessagee()
        {
           
            return Json("ee");
        }
        // Post: SujetController/deleteos/
        [HttpPost]
        public ActionResult deleteos()
        {
            var userId = Request.Form["userId"];
            var id = (Request.Form["icd"]);
            var messageuserid = int.Parse(Request.Form["messageuserid"]);
           var  msguserf = _db.MessageSujetUser.Find(messageuserid);

            _db.MessageSujetUser.Remove(msguserf);
            _db.SaveChanges();
            return Redirect("~/Sujet/updateMes/"+id+"?person=" +userId);
         //   return Json(userId,id);
        }
        // GET: SujetController/Details/5/
        public ActionResult Details(int id,int person)
        {
            var name = HttpContext.Session.GetString(SessionKeyName);
            var model = new MessageSujet
            {
                sujet = _db.Sujets.Find(id),
                messageSujetUsers = _db.MessageSujetUser.Where(b => b.Sujets.Id == id).Include(e => e.Message).OrderByDescending(e => e.Message.Id).Include(z => z.User),
                userid = person
            };
       
            return View(model);
        }


        [Route("/updates")]
        // GET: SujetController/update/5/
        public ActionResult update(int person)
        {
            var name = HttpContext.Session.GetString(SessionKeyName);
            var model = _db.User.Find(person);
     
            return View(model);
        }

        // Post: SujetController/createUserupdate
        [HttpPost]
        public ActionResult createUserupdate()
        {
            var username = Request.Form["username"];
            var adress = Request.Form["adress"];
            var password = Request.Form["password"];
            var id = int.Parse( Request.Form["id"]);
            var model = _db.User.Find(id);
            model.psuedo = username;
            model.email = adress;
            model.motpasse = password;
            model.avatar = model.avatar;
            _db.SaveChanges();

            return Redirect("~/updates/?person="+model.Id);
        }
        // Post: SujetController/CreateTest
        [HttpPost]
        public ActionResult CreateTest()
        {
            //System.Diagnostics.Debug.WriteLine("test");
            var value = Request.Form["form"];
            var userid = int.Parse( Request.Form["userId"]);
            var sjId =  int.Parse(Request.Form["sujet_id"]);
            var sujet = _db.Sujets.Find(sjId);
            var message = new Message
            {
                contenu = value.ToString(),
                CreatedAt = DateTime.Now,
            };
            _db.Messages.Add(message);
            _db.SaveChanges();
            var m = _db.Messages.Find(message.Id);
            var x = new MessageSujetUser
            {
                Sujets = sujet,
                Message = m,
                User = _db.User.Find(userid)
            };
            _db.MessageSujetUser.Add(x);
            _db.SaveChanges();
            return Redirect("~/Sujet/Details/" + sjId);
        }
      

        // POST: SujetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SujetController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SujetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SujetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SujetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
