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

namespace project_3.Controllers
{
    public class shiftadminsController : Controller
    {
        private Entities db = new Entities();

        // GET: shiftadmins
        public ActionResult Index()
        {
            var shiftadmins = db.SP_Shift_Admin_To_DataGrid();
            return View(shiftadmins);
        }

        public ActionResult Search(string Key)
        {
            if (Key != null || Key !="")
            {
                var shiftadmins = db.SP_Shift_Admin_SEARCH(Key).ToList();
                //TempData["SearchKey"] = Key;
                return View(shiftadmins);
            }
            else
            {
                return RedirectToAction("Index");
            }
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
        public async Task<ActionResult> Create(SP_Shift_Admin_To_DataGrid_Result shiftadmin)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter NewIdentity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Shift_Admin_Add_New(shiftadmin.الاسم, shiftadmin.دولة, shiftadmin.المحافظة, shiftadmin.المدينة,
                    shiftadmin.تلفون, shiftadmin.فاكس, shiftadmin.بريد_الكتروني, shiftadmin.عنوان, NewIdentity, RecFound).ToList();


                if ((int)RecFound.Value == 0)
                {
                    TempData["Msg"] = "تمت الاضافه بنجاح";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["Msg"] = "الاسم موجود بالفعل";
                    return RedirectToAction("Create");
                }

            }
            await db.SaveChangesAsync();
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", shiftadmin.معرف);
            return View(shiftadmin);
        }

        // GET: shiftadmins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var customer = await db.customers.FindAsync(id);
            var shiftadmin = db.SP_Shift_Admin_ID(id.ToString()).FirstOrDefault();
            if (shiftadmin == null)
            {
                return HttpNotFound();
            }
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", shiftadmin.معرف);
            return View(shiftadmin);
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SP_Shift_Admin_To_DataGrid_Result shiftadmin)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter NewIdentity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Shift_Admin_Update(shiftadmin.معرف, shiftadmin.الاسم, shiftadmin.دولة, shiftadmin.المحافظة,
                    shiftadmin.المدينة, shiftadmin.تلفون, shiftadmin.فاكس, shiftadmin.بريد_الكتروني, shiftadmin.عنوان,
                    NewIdentity, RecFound);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", shiftadmin.معرف);
            return View(shiftadmin);
        }
        // GET: shiftadmins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var shiftadmin = db.SP_Shift_Admin_ID(id.ToString()).FirstOrDefault();
            if (shiftadmin == null)
            {
                return HttpNotFound();
            }
            return View(shiftadmin);
        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            var shiftadmin = db.SP_Shift_Admin_ID(id.ToString()).FirstOrDefault();
            try
            {
                db.SP_Shift_Admin_DELETE(shiftadmin.رقم_المدير, shiftadmin.معرف);
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
