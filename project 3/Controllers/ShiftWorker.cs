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
    public class ShiftWorkerController : Controller
    {
        private Entities db = new Entities();

        // GET: ShiftWorker
        public ActionResult Index()
        {
            var shifts = db.SP_Shift_Main_To_ComboBox();
            ViewBag.shift_id = new SelectList(shifts, "shift_id", "shiftName");
            

            var id = db.shifts.FirstOrDefault().shift_id;
            var ShiftWorkers = db.SP_Shift_Worker_To_DataGrid_All();

            return View(ShiftWorkers);
        }

        [HttpPost]
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                var ShiftWorkers = db.SP_Shift_Worker_To_DataGrid(id);
                return PartialView("IndexUpdate",ShiftWorkers);
            }
            return View();
        }
        // GET: ShiftWorker/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shift shift = await db.shifts.FindAsync(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }


        // GET: ShiftWorker/Create
        [HttpPost]
        public ActionResult Create1(FormCollection form)
        {
            if (form["shift_id"] != null)
            {
                int SId = int.Parse(form["shift_id"]);
                return RedirectToAction("Create",new {id = SId});
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult Create(int id)
        {
            ViewBag.SID = id;
            ViewBag.Products = db.SP_Shift_Main_ID(id.ToString());
            var shifts = db.SP_Shift_Main_To_ComboBox();
            ViewBag.shift_id = new SelectList(shifts, "shift_id", "shiftName", id);
            var workers = db.SP_Worker_To_ComboBox();
            ViewBag.worker_id = new SelectList(workers, "worker_id", "firstName");
            return View();
        }

        // POST: ShiftWorker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SP_Shift_Worker_To_DataGrid_Result shift)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter NewIdentity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Shift_Worker_Add_New(shift.رقم_الوردية, shift.رقم_العامل,
                    NewIdentity, RecFound).ToList();
                if ((int)RecFound.Value == 0)
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Msg"] = "العامل موجود بالفعل";
                    TempData["id"] = TempData["id2"];
                    TempData["id2"] = TempData["id"];
                    return RedirectToAction("Create");
                }
            }

            var shifts = db.SP_Shift_Main_To_ComboBox();
            ViewBag.shift_id = new SelectList(shifts, "shift_id", "shiftName", TempData["id"]);
            var workers = db.SP_Worker_To_ComboBox();
            ViewBag.worker_id = new SelectList(workers, "worker_id", "firstName");
            return View(shift);
        }

        // GET: ShiftWorker/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shift shift = await db.shifts.FindAsync(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            ViewBag.shiftAdmin_shiftAdmin_id = new SelectList(db.shiftadmins, "shiftAdmin_id", "shiftAdmin_id", shift.shiftAdmin_shiftAdmin_id);
            return View(shift);
        }

        // POST: ShiftWorker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "shift_id,shiftName,shiftAdmin_shiftAdmin_id")] shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shift).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.shiftAdmin_shiftAdmin_id = new SelectList(db.shiftadmins, "shiftAdmin_id", "shiftAdmin_id", shift.shiftAdmin_shiftAdmin_id);
            return View(shift);
        }

        // GET: ShiftWorker/Delete/5
        public ActionResult Delete(int? shift_id, int? worker_id)
        {
            if (shift_id == null || worker_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shiftWorker = db.SP_Shift_Worker_ID(shift_id, worker_id).FirstOrDefault();
            if (shiftWorker == null)
            {
                return HttpNotFound();
            }
            return View(shiftWorker);
        }

        // POST: ShiftWorker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(SP_Shift_Worker_ID_Result shiftWorker)
        {
            //var shiftWorker = db.SP_Shift_Worker_ID(id, WorkerId).FirstOrDefault();
            try
            {
                db.SP_Shift_Worker_DELETE(shiftWorker.shift_shift_id, shiftWorker.workers_worker_id);
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
