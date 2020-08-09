using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using project_3.Models;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;

namespace project_3.Controllers
{
    public class CustomersController : Controller
    {
        private Entities db = new Entities();

        // GET: customers
        public async Task<ActionResult> Index()
        {
            var customers = db.Database.SqlQuery<CustomerViewModel>("SP_Customer_To_DataGrid");
            return View(await customers.ToListAsync());
        }

        public ActionResult Search(string Key)
        {
            if(Key!=null)
            {
                var customers = db.SP_Customer_Search(Key).ToList();
                //TempData["SearchKey"] = Key;
                return View(customers);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = await db.customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: customers/Create
        public ActionResult Create()
        {
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName");
            return View();
        }

        // POST: customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));              
                ObjectParameter NewIdentity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Customer_Add_New(customer.الاسم, customer.دولة, customer.المحافظة, customer.المدينة,
                    customer.تلفون, customer.فاكس, customer.بريد_الكتروني, customer.عنوان, NewIdentity, RecFound).ToList();
               
               
                if((int)RecFound.Value==0)
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
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", customer.معرف);
            return View(customer);
        }

        // GET: customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var customer = await db.customers.FindAsync(id);
            var customer = db.SP_Customer_ID(id).FirstOrDefault(); 
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", customer.معرف);
            return View(customer);
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter NewIdentity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Customer_Update(customer.معرف, customer.الاسم, customer.دولة, customer.المحافظة,
                    customer.المدينة, customer.تلفون, customer.فاكس, customer.بريد_الكتروني, customer.عنوان,
                    NewIdentity, RecFound);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.address_add_id = new SelectList(db.addresses, "add_id", "firstName", customer.معرف);
            return View(customer);
        }

        // GET: customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            var customer = db.SP_Customer_ID(id).FirstOrDefault();
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            
            var customer = db.SP_Customer_ID(id).FirstOrDefault();
            try
            {
                db.SP_Customer_Delete(customer.رقم_الوكيل, customer.معرف);
                await db.SaveChangesAsync();
            }
            catch(Exception e)
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
