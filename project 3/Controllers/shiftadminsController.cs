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
    public class shiftadminsController : Controller
    {
        private Entities11 db = new Entities11();

        // GET: shiftadmins
        public ActionResult Index()
        {
            //var shiftadmins = db.shiftadmins.Include(s => s.address);
            var shiftadmins = db.SP_Shift_Admin_To_DataGrid();
            return View(shiftadmins);
        }

        // GET: shiftadmins/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shiftadmin shiftadmin = await db.shiftadmins.FindAsync(id);
            if (shiftadmin == null)
            {
                return HttpNotFound();
            }
            return View(shiftadmin);
        }

        // GET: shiftadmins/Create
        public ActionResult Create()
        {
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName");
            return View();
        }

        // POST: shiftadmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "shiftAdmin_id,address_add_id")] shiftadmin shiftadmin)
        {
            if (ModelState.IsValid)
            {
                db.shiftadmins.Add(shiftadmin);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", shiftadmin.address_add_id);
            return View(shiftadmin);
        }

        // GET: shiftadmins/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shiftadmin shiftadmin = await db.shiftadmins.FindAsync(id);
            if (shiftadmin == null)
            {
                return HttpNotFound();
            }
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", shiftadmin.address_add_id);
            return View(shiftadmin);
        }

        // POST: shiftadmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "shiftAdmin_id,address_add_id")] shiftadmin shiftadmin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shiftadmin).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", shiftadmin.address_add_id);
            return View(shiftadmin);
        }

        // GET: shiftadmins/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shiftadmin shiftadmin = await db.shiftadmins.FindAsync(id);
            if (shiftadmin == null)
            {
                return HttpNotFound();
            }
            return View(shiftadmin);
        }

        // POST: shiftadmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            shiftadmin shiftadmin = await db.shiftadmins.FindAsync(id);
            db.shiftadmins.Remove(shiftadmin);
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
