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
    public class storesController : Controller
    {
        private Entities db = new Entities();

        // GET: stores
        public ActionResult Index()
        {
            var stores = db.Database.SqlQuery<SP_Store_Search_Result>("SP_Store_To_DataGrid");
            return View(stores.ToList());
        }
        public ActionResult Search(string Key)
        {
            if (Key != null)
            {
                var stores = db.SP_Store_Search(Key).ToList();
                return View(stores);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: stores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store store = await db.stores.FindAsync(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(store store)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter rec_found = new ObjectParameter("rec_found", typeof(int));
                db.SP_Store_Add_New(User.Identity.GetUserId(),store.storeName," ",store.storeAdminName,store.storeLocation, rec_found).ToList();

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
            return View(store);
        }

        // GET: stores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var store = db.SP_Store_ID(id).FirstOrDefault();
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(store store)
        {
            if (ModelState.IsValid)
            {
                db.SP_Store_Update(User.Identity.GetUserId(),store.store_id, store.storeName," ",store.storeAdminName , store.storeLocation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(store);
        }

        // GET: stores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var store = db.SP_Store_ID(id).FirstOrDefault();
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var store = db.SP_Store_Search(id.ToString()).FirstOrDefault();
            try
            {
                db.SP_Store_Delete(User.Identity.GetUserId(),store.store_id);
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
