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
using project_3.Models;
using System.Data.Entity.Core.Objects;

namespace project_3.Controllers
{
    public class transvehcilesController : Controller
    {
        private Entities db = new Entities();

        // GET: transvehciles
        public async Task<ActionResult> Index()
        {
            var transvehciles = db.Database.SqlQuery<TransVehicleViewModel>("SP_Trans_Vin_To_DataGrid");
            return View(await transvehciles.ToListAsync());
        }
        public ActionResult Search(string Key)
        {
            if(Key!=null)
            {
                var vehicles = db.SP_TRANS_VIN_Search(Key).ToList();
                //TempData["SearchKey"] = Key;
                return View(vehicles);
            }
            else
            {
                return RedirectToAction("Index");
            }
            

        }

        // GET: transvehciles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transvehcile transvehcile = await db.transvehciles.FindAsync(id);
            if (transvehcile == null)
            {
                return HttpNotFound();
            }
            return View(transvehcile);
        }

        // GET: transvehciles/Create
        public ActionResult Create()
        {
            var Customers = db.SP_Customer_To_ComboBox();
            ViewBag.customers_Customers_id = new SelectList(Customers, "Customers_id", "firstName");
            return View();
        }

        // POST: transvehciles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TransVehicleViewModel transvehcile)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter NewIdentity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Trans_Vin_Add_New(transvehcile.اسم_السائق, transvehcile.رقم_العربية, transvehcile.الموديل,
                    transvehcile.هاتف, transvehcile.رقم_الوكيل , transvehcile.العنوان, transvehcile.سريال_العربية,
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

            var Customers = db.SP_Customer_To_ComboBox();
            ViewBag.customers_Customers_id = new SelectList(Customers, "Customers_id", "firstName");
            return View(transvehcile);
        }

        // GET: transvehciles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transvehcile =  db.SP_Trans_Vin_ID(id).FirstOrDefault();
            if (transvehcile == null)
            {
                return HttpNotFound();
            }
            var Customers = db.SP_Customer_To_ComboBox();
            ViewBag.customers_Customers_id = new SelectList(Customers, "Customers_id", "firstName");
            return View(transvehcile);
        }

        // POST: transvehciles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TransVehicleViewModel transvehcile)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                db.SP_Trans_vin_Update(transvehcile.الرقم,transvehcile.اسم_السائق,transvehcile.رقم_العربية,
                    transvehcile.الموديل,transvehcile.هاتف,transvehcile.رقم_الوكيل,transvehcile.العنوان,
                    transvehcile.سريال_العربية,RecFound);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var Customers = db.SP_Customer_To_ComboBox();
            ViewBag.customers_Customers_id = new SelectList(Customers, "رقم_الوكيل", "firstName");
            return View(transvehcile);
        }

        // GET: transvehciles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transvehcile = db.SP_Trans_Vin_ID(id).FirstOrDefault();
            if (transvehcile == null)
            {
                return HttpNotFound();
            }
            return View(transvehcile);
        }

        // POST: transvehciles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var transvehcile =  db.SP_Trans_Vin_ID(id).FirstOrDefault();
            //db.transvehciles.Remove(transvehcile);
            try
            {
                db.SP_Trans_Vin_DELETE(transvehcile.الرقم);
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
