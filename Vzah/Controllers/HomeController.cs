using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vzah.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Vzah-Home";
            return View();
        }
        public ActionResult AboutUs()
        {
            ViewBag.Title = "Vzah-About Us";
            return View();
        }
        public ActionResult FAQ()
        {
            ViewBag.Title = "Vzah-FAQ's";
            return View();
        }
        public ActionResult Blogs()
        {
            ViewBag.Title = "Vzah-Blogs";
            return View();
        }
        public ActionResult News()
        {
            ViewBag.Title = "Vzah-News";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Title = "Vzah-Contact Us";
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Title = "Vzah-Login/Register";
            return View();
        }
    }
}
