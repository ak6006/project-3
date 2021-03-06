﻿    using System;
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
    public class workersController : Controller
    {
        private Entities db = new Entities();

        // GET: workers
        public ActionResult Index()
        {
            var workers = db.SP_Workers_To_DataGrid();
            return View(workers);
        }

        public ActionResult Search(string Key)
        {
            if (Key != null)
            {
                var worker = db.SP_Worker_SEARCH(Key).ToList();
                return View(worker);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: workers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var worker =  db.SP_Worker_ID(id).SingleOrDefault();
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        // GET: workers/Create
        public ActionResult Create()
        {
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName");
            return View();
        }

        // POST: workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SP_Worker_ID_Result worker)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter NewIdentity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Worker_Add_New(User.Identity.GetUserId(),worker.الاسم, worker.دولة, worker.المحافظة, worker.المدينة,
                    worker.تلفون, worker.فاكس, worker.بريد_الكتروني, worker.عنوان, worker.id_card_number,
                    worker.relative_name_person_A, worker.relative_phone_person_A, worker.relative_name_person_B,
                    worker.relative_phone_person_B,worker.relative_casen_person_A,worker.relative_casen_person_B
                    ,worker.job_titel, NewIdentity, RecFound).ToList();


                if ((int)RecFound.Value == 0)
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
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", worker.معرف);
            return View(worker);
        }

        // GET: workers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var customer = await db.customers.FindAsync(id);
            var worker = db.SP_Worker_ID(id).FirstOrDefault();
            if (worker == null)
            {
                return HttpNotFound();
            }
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", worker.معرف);
            return View(worker);
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SP_Worker_ID_Result worker)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter NewIdentity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Worker_Update(User.Identity.GetUserId(),worker.معرف, worker.الاسم, worker.دولة, worker.المحافظة,
                    worker.المدينة, worker.تلفون, worker.فاكس, worker.بريد_الكتروني, worker.عنوان,
                    worker.id_card_number,worker.relative_name_person_A,worker.relative_phone_person_A,
                    worker.relative_name_person_B,worker.relative_phone_person_B,worker.relative_casen_person_A
                    ,worker.relative_casen_person_B,worker.job_titel,NewIdentity, RecFound);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", worker.معرف);
            return View(worker);
        }
        // GET: workers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var worker = db.SP_Worker_ID(id).FirstOrDefault();
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            var worker = db.SP_Worker_ID(id).FirstOrDefault();
            try
            {
                db.SP_Worker_DELETE(User.Identity.GetUserId(), worker.رقم_العامل, worker.معرف);
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
