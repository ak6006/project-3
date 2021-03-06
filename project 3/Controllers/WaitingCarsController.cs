﻿using project_3.Models;
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

        public ActionResult Update()
        {
            var Cars = db.SP_Sales_Order_Trans_Vin_display_update().ToList();
            return View(Cars);
        }

        [HttpPost]
        public ActionResult Edit(SP_Sales_Order_Trans_Vin_display_update_Result order)
        {
            var order1 = db.orders.Find(order.order_id);
            order1.date = order.date;
            db.SaveChanges();
            var Cars = db.SP_Sales_Order_Trans_Vin_display_update().ToList();
            return View("Update",Cars);
        }
    }
}