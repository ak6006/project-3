using project_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project_3.Controllers
{
    public class WaitingCarsController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCars()
        {
            var Cars = db.SP_Sales_Order_Trans_Vin_display().ToList();
            return View(Cars);
        }
    }
}