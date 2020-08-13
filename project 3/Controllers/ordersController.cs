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

namespace project_3.Controllers
{
    public class ordersController : Controller
    {
        private Entities db = new Entities();

        // GET: orders
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

        // GET: orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = await db.orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: orders/Create
        public ActionResult Create()
        {
            var customers = db.SP_Customer_To_ComboBox();
            ViewBag.Customers_id = new SelectList(customers, "Customers_id", "firstName");

            var stores = db.SP_Store_To_ComboBox();
            ViewBag.Stores_id = new SelectList(stores, "store_id", "storeName");
            
            return View();
        }

        // POST: orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SP_Order_To_DataGrid_Result order)
        {
            if (ModelState.IsValid)
            {
                db.SP_Order_Add_New(order.رقم_الوكيل, order.رقم_المخزن, order.التاريخ, order.ملاحظة);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var customers = db.SP_Customer_To_ComboBox();
            ViewBag.Customers_id = new SelectList(customers, "Customers_id", "firstName");

            var stores = db.SP_Store_To_ComboBox();
            ViewBag.Stores_id = new SelectList(stores, "store_id", "storeName");
            return View(order);
        }

        // GET: orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order =db.SP_Order_ID(id).FirstOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }
            var order1 = db.orders.Find(id);
            var customers = db.SP_Customer_To_ComboBox();
            ViewBag.Customers_id = new SelectList(customers, "Customers_id", "firstName",order1.customer.Customers_id);

            var stores = db.SP_Store_To_ComboBox();
            ViewBag.Stores_id = new SelectList(stores, "store_id", "storeName",order1.store.store_id);
            return View(order1);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(order order)
        {
            if (ModelState.IsValid)
            {
                db.SP_Order_Update(order.order_id, order.customers_Customers_id, order.store_store_id, order.date, order.notic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var customers = db.SP_Customer_To_ComboBox();
            ViewBag.Customers_id = new SelectList(customers, "Customers_id", "firstName");

            var stores = db.SP_Store_To_ComboBox();
            ViewBag.Stores_id = new SelectList(stores, "store_id", "storeName");
            return View(order);
        }

        // GET: orders/Delete/5
        public  ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = db.SP_Order_ID(id).FirstOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var order = db.SP_Order_ID(id).FirstOrDefault();
            try
            {
                db.SP_Order_DELETE(order.order_id);
                await db.SaveChangesAsync();
            }
            catch
            {
                TempData["Msg"] = "لا يمكن الحذف لارتباطه بسجلات اخرى";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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
