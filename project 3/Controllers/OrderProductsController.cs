using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using project_3;
using project_3.Models;
using Microsoft.AspNet.Identity;

namespace project_3.Controllers
{
    public class OrderProductsController : Controller
    {
        private Entities db = new Entities();

        // GET: OrderProducts
        public ActionResult Index()
        {
            var orders = db.SP_Order_To_DataGrid();
            return View(orders);
        }
        public ActionResult Search(string Key)
        {
            if (Key != null)
            {
                var customers = db.SP_Order_Search(Key).ToList();
                //TempData["SearchKey"] = Key;
                return View(customers);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult GetProducts(int id)
        {
            
            var products = db.SP_Order_Items_To_DataGrid(id);
            ViewBag.OrderId = id;
            var ProductList = db.SP_Order_ID(id);
            ViewBag.CustomerName = ProductList.FirstOrDefault().firstName;
            return View(products);
        }
        // GET: OrderProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_has_product order_has_product = await db.order_has_product.FindAsync(id);
            if (order_has_product == null)
            {
                return HttpNotFound();
            }
            return View(order_has_product);
        }


        [HttpPost]
        public ActionResult Create1(FormCollection form)
        {
            if (form["ThisOrderId"] != null && form["CustomerName"] != null)
            {
                int OID = int.Parse(form["ThisOrderId"]);
                string CustName = form["CustomerName"];
                return RedirectToAction("Create",new {id = OID , name = CustName });
            }
            else
            {
                return HttpNotFound();
            }
        }
        // GET: OrderProducts/Create
        public ActionResult Create(int id , string name)
        {
            ViewBag.CustomerName = name;
            ViewBag.ThisOrderId = id;
            var Products = db.SP_Product_To_ComboBox();
            ViewBag.Product_Id = new SelectList(Products, "product_id", "productName");
            var Measurements = db.SP_Measurement_To_ComboBox();
            ViewBag.Measurements_Id = new SelectList(Measurements, "measure_id", "measre_name");
            var weights = db.SP_weight_To_ComboBox();
            ViewBag.Weight_Id = new SelectList(weights, "weight_id", "weight_net");
            return View();
        }

        // POST: OrderProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SP_Order_Items_To_DataGrid_Result order, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.SP_Order_Items_Add_New(User.Identity.GetUserId(),order.رقم_الطلبية, order.product_id, order.measure_id, order.weight_id, order.الكمية);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    @TempData["Msg"] = "هذا المنتج موجود بالفعل";
                    return RedirectToAction("Index");
                }

            }

            var Products = db.SP_Product_To_ComboBox();
            ViewBag.Product_Id = new SelectList(Products, "product_id", "productName");
            var Measurements = db.SP_Measurement_To_ComboBox();
            ViewBag.Measurements_Id = new SelectList(Measurements, "measure_id", "measre_name");
            var weights = db.SP_weight_To_ComboBox();
            ViewBag.Weight_Id = new SelectList(weights, "weight_id", "weight_net");
            return View(order);
        }

        // GET: OrderProducts/Edit/5
        public ActionResult Edit(int? OId,int? PId , int? MId , int? WId, string CName)
        {
            if (OId == null || PId == null || MId == null || WId == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.SP_Order_Items_ID(OId , PId , MId , WId).FirstOrDefault();
            var OldOrder = db.SP_Order_Items_ID(OId, PId, MId, WId).FirstOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }
            TempData["OId"] = OldOrder.رقم_الطلبية;
            TempData["PId"] = OldOrder.product_id;
            TempData["MId"] = OldOrder.measure_id;
            TempData["WId"] = OldOrder.weight_id;
            ViewBag.ThisOrderId = OldOrder.رقم_الطلبية;
            ViewBag.CustomerName = OldOrder.اسم_العميل;
            var Products = db.SP_Product_To_ComboBox();
            ViewBag.Products_Id = new SelectList(Products, "product_id", "productName");
            var Measurements = db.SP_Measurement_To_ComboBox();
            ViewBag.Measurements_Id = new SelectList(Measurements, "measure_id", "measre_name");
            var weights = db.SP_weight_To_ComboBox();
            ViewBag.Weights_Id = new SelectList(weights, "weight_id", "weight_net");
            return View(order);
        }

        // POST: OrderProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SP_Order_Items_ID_Result order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.SP_Order_Items_Update(User.Identity.GetUserId(),int.Parse(TempData["OId"].ToString()), int.Parse(TempData["PId"].ToString())
                        , int.Parse(TempData["MId"].ToString()), int.Parse(TempData["WId"].ToString()),
                         order.product_id, order.measure_id, order.weight_id,order.الكمية);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    @TempData["Msg"] = "هذا المنتج موجود بالفعل";
                    return RedirectToAction("Index");
                }
                
            }
            var Products = db.SP_Product_To_ComboBox();
            ViewBag.Product_Id = new SelectList(Products, "product_id", "productName");
            var Measurements = db.SP_Measurement_To_ComboBox();
            ViewBag.Measurements_Id = new SelectList(Measurements, "measure_id", "measre_name");
            var weights = db.SP_weight_To_ComboBox();
            ViewBag.Weight_Id = new SelectList(weights, "weight_id", "weight_net");
            return View(order);
        }

        // GET: OrderProducts/Delete/5
        public ActionResult Delete(int? OId, int? PId, int? MId, int? WId, string CName)
        {
            if (OId == null || PId == null || MId == null || WId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.SP_Order_Items_ID(OId, PId, MId, WId).FirstOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(SP_Order_Items_ID_Result order)
        {
            try
            {
                db.SP_Order_Items_DELETE(User.Identity.GetUserId(),order.رقم_الطلبية, order.product_id, order.measure_id, order.weight_id);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Msg"] = "لا يمكن الحذذف لارتباطه بملفات اخرى";
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
