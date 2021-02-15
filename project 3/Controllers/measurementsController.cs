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
using Microsoft.AspNet.Identity;

namespace project_3.Controllers
{
    public class measurementsController : Controller
    {
        private Entities db = new Entities();

        // GET: measurements
        public ActionResult Index()
        {
            var measurements = db.SP_Measurement_To_DataGrid();
            return View(measurements);
        }
        public ActionResult Search(string Key)
        {
            if (Key != null || Key!="")
            {
                var measurements = db.SP_Measurement_Search(Key).ToList();
                return View(measurements);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: measurements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            measurement measurement = await db.measurements.FindAsync(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        // GET: measurements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: measurements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MeasurementViewModel measurement)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter rec_found = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter new_identity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Measurement_Add_New(User.Identity.GetUserId(),measurement.اسم_وحدة_القياس,measurement.measurement_Count_per_unit, new_identity, rec_found).ToList();

                if ((int)rec_found.Value == 0)
                {
                    TempData["Msg"] = "تمت الاضافه بنجاح";
                    TempData["Color"] = "Green";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["Msg"] = "الاسم موجود بالفعل";
                    TempData["Color"] = "Red";
                    return RedirectToAction("Create");
                }
            }
            await db.SaveChangesAsync();
            return View(measurement);
        }

        // GET: measurements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var measurement = db.SP_Measurement_ID(id).FirstOrDefault();
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        // POST: measurements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( MeasurementViewModel measurement)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter rec_found = new ObjectParameter("rec_found", typeof(int));
                db.SP_Measurement_Update(User.Identity.GetUserId(),measurement.رقم_الوحدة, measurement.اسم_وحدة_القياس, measurement.measurement_Count_per_unit, rec_found);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(measurement);
        }

        // GET: measurements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var measurement = db.SP_Measurement_ID(id).FirstOrDefault();
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        // POST: measurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var measurement =  db.SP_Measurement_ID(id).FirstOrDefault();
            try
            {
                db.SP_Measurement_DELETE(User.Identity.GetUserId(),measurement.رقم_الوحدة);
                await db.SaveChangesAsync();
            }
            catch
            {
                TempData["Msg"] = "لا يمكن الحذف لارتباطه بسجلات اخرى";
                TempData["Color"] = "Red";
                return RedirectToAction("Index");
            }
            TempData["Msg"] = "تم الحذف بنجاح";
            TempData["Color"] = "Green";
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
