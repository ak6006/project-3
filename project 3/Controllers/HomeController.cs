using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project_3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult Accounting()
        {
            return View();
        }
        public ActionResult Production()
        {
            return View();
        }
        public ActionResult Purchases()
        {
            return View();
        }
        public ActionResult Employees()
        {
            return View();
        }
        public ActionResult System()
        {
            return View();
        }
        public ActionResult Stores()
        {
            return View();
        }
        public ActionResult Sales()
        {
            return View();
        }

        public ActionResult Resources()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      
    }
}