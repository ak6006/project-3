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
    public class OrderTransController : Controller
    {
        private Entities db = new Entities();

        // GET: transvehcile_has_order
        public ActionResult Index()
        {
            var orders = db.SP_Order_To_DataGrid();
            return View(orders);
        }

        public ActionResult Search(string Key)
        {
            if (Key != null)
            {
                var orders = db.SP_Order_Search(Key).ToList();
                return View(orders);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult GetVehicles(int id)
        {
            var Vehicles = db.SP_Order_Trans_Vin_To_DataGrid(id);
            ViewBag.OrderId = id;
            var OrderList = db.SP_Order_ID(id);
            ViewBag.CustomerId = OrderList.FirstOrDefault().Customers_id;
            return View(Vehicles);
        }

        // GET: transvehcile_has_order/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transvehcile_has_order transvehcile_has_order = await db.transvehcile_has_order.FindAsync(id);
            if (transvehcile_has_order == null)
            {
                return HttpNotFound();
            }
            return View(transvehcile_has_order);
        }

        [HttpPost]
        public ActionResult Create1(FormCollection form)
        {
            if (form["ThisOrderId"] != null && form["CustomerId"] != null)
            {
                int OID = int.Parse(form["ThisOrderId"]);
                int CustId = int.Parse(form["CustomerId"]);
                return RedirectToAction("Create", new { id = OID, CustId = CustId });
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: transvehcile_has_order/Create
        public ActionResult Create(int? id ,int? CustId)
        {
            ViewBag.ThisOrderId = id;
            var Vehicles = db.SP_Trans_Vin_To_ComboBox(CustId);
            ViewBag.Trans_Id = new SelectList(Vehicles, "v_id", "transVehcile_driver_name");
            return View();
        }

        // POST: transvehcile_has_order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SP_Order_Trans_Vin_To_DataGrid_Result OrderVehicle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.SP_Order_Trans_Vin_Add_New(OrderVehicle.رقم_الطلبية, OrderVehicle.v_id);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    @TempData["Msg"] = "هذا السائق موجود بالفعل";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: transvehcile_has_order/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transvehcile_has_order transvehcile_has_order = await db.transvehcile_has_order.FindAsync(id);
            if (transvehcile_has_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_order_id = new SelectList(db.orders, "order_id", "notic", transvehcile_has_order.order_order_id);
            return View(transvehcile_has_order);
        }

        // POST: transvehcile_has_order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "transVehcile_v_id,order_order_id,transVehcile_has_order_state")] transvehcile_has_order transvehcile_has_order)
        {
            if (ModelState.IsValid)
            {
                //db.SP_Order_Trans_Vin_Update();
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.order_order_id = new SelectList(db.orders, "order_id", "notic", transvehcile_has_order.order_order_id);
            return View(transvehcile_has_order);
        }

        // GET: transvehcile_has_order/Delete/5
        public ActionResult Delete(int? OId , int? VId , string Driver)
        {
            if (OId == null || VId == null || Driver == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var OrderVehicle = db.SP_Order_Trans_Vin_ID(OId, VId).FirstOrDefault(); 
            
            return View(OrderVehicle);
        }

        // POST: transvehcile_has_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(SP_Order_Trans_Vin_ID_Result OrderVehicle)
        {
            try
            {
                db.SP_Order_Trans_Vin_DELETE(OrderVehicle.رقم_الطلبية, OrderVehicle.v_id);
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
