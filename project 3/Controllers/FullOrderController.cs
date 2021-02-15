using Microsoft.AspNet.Identity;
using project_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project_3.Controllers
{
    public class FullOrderController : Controller
    {
        private Entities db = new Entities();

        // GET: FullOrder
        public ActionResult Index()
        {
            return View();
        }

        // GET: FullOrder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FullOrder/Create
        public ActionResult Create()
        {
            var customers = db.SP_Customer_To_ComboBox();
            ViewBag.Customers_id = new SelectList(customers, "Customers_id", "firstName");

            return View();
        }

        public ActionResult GetCars(int CustId)
        {
            var Products = db.SP_Product_To_ComboBox();
            ViewBag.Product_Id = new SelectList(Products, "product_id", "productName");

            var Measurements = db.SP_Measurement_To_ComboBox();
            ViewBag.Measurements_Id = new SelectList(Measurements, "measure_id", "measre_name");

            var weights = db.SP_weight_To_ComboBox();
            ViewBag.Weight_Id = new SelectList(weights, "weight_id", "weight_net");

            var stores = db.SP_Store_To_ComboBox();
            ViewBag.Stores_id = new SelectList(stores, "store_id", "storeName");

            var Cars = db.SP_Trans_Vin_To_ComboBox(CustId);
            ViewBag.Car_Id = new SelectList(Cars, "v_id", "transVehcile_driver_name");

            ViewBag.CustId = CustId;
            return View();
        }



        // POST: FullOrder/Create
        [HttpPost]
        public ActionResult Create(CustomerOrderViewModel order)
        {
            try
            {
                db.SP_Flutter_Order_Add_New(User.Identity.GetUserId(),order.CustomerId, order.StoreId, order.OrderDate, order.Notes,
                    order.ProductId, order.MeasureId, order.WieghtId,int.Parse(order.quantity), order.CarId.ToString());

                return RedirectToAction("Index","orders");
            }
            catch
            {
                return View();
            }
        }

        // GET: FullOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FullOrder/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FullOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FullOrder/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
