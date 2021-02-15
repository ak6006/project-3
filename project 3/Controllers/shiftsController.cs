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
    public class shiftsController : Controller
    {
        private Entities db = new Entities();

        // GET: shifts
        public ActionResult Index()
        {
            var shifts = db.SP_Shift_Main_To_DataGrid();
            return View(shifts);
        }

        public ActionResult Search(string Key)
        {
            if (Key != null)
            {
                var shifts = db.SP_Shift_Main_Search(Key).ToList();
                //TempData["SearchKey"] = Key;
                return View(shifts);
            }
            else
            {
                return RedirectToAction("Index");
            }


        }
        // GET: shifts/Details/5
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

        // GET: shifts/Create
        public ActionResult Create()
        {
            var admins = db.SP_Shift_Admin_To_ComboBox();
            ViewBag.shift_admin_id = new SelectList(admins, "shiftAdmin_id", "firstName");
            return View();
        }

        // POST: transvehciles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SP_Shift_Main_To_DataGrid_Result shift)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter NewIdentity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Shift_Main_Add_New(User.Identity.GetUserId(), shift.اسم_الوردية, shift.رقم_مدير_الوردية,
                    NewIdentity, RecFound).ToList();
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

            var admins = db.SP_Shift_Admin_To_ComboBox();
            ViewBag.shift_admin_id = new SelectList(admins, "shiftAdmin_id", "firstName");
            return View(shift);
        }

        // GET: shifts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shift = db.SP_Shift_Main_ID(id.ToString()).FirstOrDefault();
            if (shift == null)
            {
                return HttpNotFound();
            }
            var admins = db.SP_Shift_Admin_To_ComboBox();
            ViewBag.shift_admin_id = new SelectList(admins, "shiftAdmin_id", "firstName");
            return View(shift);
        }

        // POST: transvehciles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SP_Shift_Main_To_DataGrid_Result shift)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                db.SP_Shift_Main_Update(User.Identity.GetUserId(),shift.رقم_الوردية,shift.رقم_مدير_الوردية,shift.اسم_الوردية, RecFound);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var admins = db.SP_Shift_Admin_To_ComboBox();
            ViewBag.shift_admin_id = new SelectList(admins, "shiftAdmin_id", "firstName");
            return View(shift);
        }


        // GET: shifts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shift = db.SP_Shift_Main_ID(id.ToString()).FirstOrDefault();
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // POST: transvehciles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var shift = db.SP_Shift_Main_ID(id.ToString()).FirstOrDefault();
            //db.transvehciles.Remove(transvehcile);
            try
            {
                db.SP_Shift_Main_DELETE(User.Identity.GetUserId(),shift.رقم_الوردية);
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
