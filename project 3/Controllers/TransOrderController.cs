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
using System.Data.Entity.Core.Objects;
using project_3.Models;

namespace project_3.Controllers
{
    public class TransOrderController : Controller
    {
        private Entities db = new Entities();

        // GET: transvehcile_has_order
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Search(string CarSerial)
        {
            var result = db.SP_Sales_Order_Trans_Vin_Load(CarSerial);
            return View(result);
        }

        
        public ActionResult Transport(int OId , int PId , int WId , string CName , string DName , int All , int Remaining)
        {
            //ViewBag.OId = OId;
            //ViewBag.PId = PId;
            //ViewBag.WId = WId;
            //ViewBag.CName = CName;
            //ViewBag.DName = DName;
            //ViewBag.All = All;
            //ViewBag.Remaining = Remaining;
            OrderVehicleViewModel result = new OrderVehicleViewModel
            {
                OId = OId,
                PId = PId,
                WId = WId,
                CName = CName,
                DName = DName,
                All = All,
                Remaining = Remaining
            };
            return View(result);
        }

        [HttpPost]
        public ActionResult Transport2(OrderVehicleViewModel record)
        {
            ObjectParameter SerialFound = new ObjectParameter("serial_found", typeof(int));
            ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
            ObjectParameter NetCounter = new ObjectParameter("netCounter", typeof(int));
            
            try
            {
                db.SP_Sales_BarCode(record.BarCode, record.OId.ToString(), record.PId.ToString()
                , record.WId.ToString(), SerialFound, RecFound, NetCounter);

                ViewBag.OId = record.OId;
                ViewBag.PId = record.PId;
                ViewBag.WId = record.WId;
                ViewBag.CName = record.CName;
                ViewBag.DName = record.DName;
                ViewBag.All = record.All;
                ViewBag.Remaining = record.Remaining;
                record.Counter = (int)NetCounter.Value;

                if ((int)RecFound.Value == 0)
                {
                    TempData["Msg"] = "السريال غير صحيح";
                    return RedirectToAction("Transport",record);
                }
                else if ((int)NetCounter.Value == 0)
                {
                    TempData["Msg"] = "تم انتهاء التحميل بالفعل";
                    return RedirectToAction("Transport", record);
                }
                else if ((int)SerialFound.Value > 0)
                {
                    TempData["Msg"] = "تم بيع الشكارة بالفعل";
                    return RedirectToAction("Transport", record);
                }

                else
                {
                    TempData["Msg"] = "تمت الاضافه بنجاح";
                    db.SaveChangesAsync();
                    return RedirectToAction("Transport", record);
                }
            }
            catch
            {
                return RedirectToAction("Transport", record);
            }
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

        // GET: transvehcile_has_order/Create
        public ActionResult Create()
        {
            ViewBag.order_order_id = new SelectList(db.orders, "order_id", "notic");
            return View();
        }

        // POST: transvehcile_has_order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "transVehcile_v_id,order_order_id,transVehcile_has_order_state")] transvehcile_has_order transvehcile_has_order)
        {
            if (ModelState.IsValid)
            {
                db.transvehcile_has_order.Add(transvehcile_has_order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.order_order_id = new SelectList(db.orders, "order_id", "notic", transvehcile_has_order.order_order_id);
            return View(transvehcile_has_order);
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
                db.Entry(transvehcile_has_order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.order_order_id = new SelectList(db.orders, "order_id", "notic", transvehcile_has_order.order_order_id);
            return View(transvehcile_has_order);
        }

        // GET: transvehcile_has_order/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: transvehcile_has_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            transvehcile_has_order transvehcile_has_order = await db.transvehcile_has_order.FindAsync(id);
            db.transvehcile_has_order.Remove(transvehcile_has_order);
            await db.SaveChangesAsync();
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
