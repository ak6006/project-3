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
    public class StoreProductController : Controller
    {
        private Entities db = new Entities();

        // GET: StoreProduct
        public ActionResult Index()
        {
            var Products = db.SP_Product_To_ComboBox();
            ViewBag.ProductId = new SelectList(Products, "product_id", "productName");
            var weights = db.SP_weight_To_ComboBox();
            ViewBag.WeightId = new SelectList(weights, "weight_id", "weight_net");
            var stores = db.SP_Store_To_ComboBox();
            ViewBag.StoreId = new SelectList(stores, "store_id", "storeName");
            var Shifts = db.SP_Shift_Main_To_ComboBox();
            ViewBag.ShiftId = new SelectList(Shifts, "shift_id", "shiftName");
            return View();
        }
        
        
        
        public ActionResult Index2(store_has_product SProd)
        {
            var Products = db.SP_Product_To_ComboBox();
            ViewBag.ProductId = new SelectList(Products, "product_id", "productName");
            var weights = db.SP_weight_To_ComboBox();
            ViewBag.WeightId = new SelectList(weights, "weight_id", "weight_net");
            var stores = db.SP_Store_To_ComboBox();
            ViewBag.StoreId = new SelectList(stores, "store_id", "storeName");
            var Shifts = db.SP_Shift_Main_To_ComboBox();
            ViewBag.ShiftId = new SelectList(Shifts, "shift_id", "shiftName");
            ViewBag.SelectedShiftId = SProd.shift_shift_id;
            ViewBag.SelectedShift = db.shifts.Find(SProd.shift_shift_id).shiftName;
            ViewBag.SelectedProdId = SProd.product_product_id;
            ViewBag.SelectedProd = db.products.Find(SProd.product_product_id).productName;
            ViewBag.SelectedWieghtId = SProd.weight_weight_id;
            ViewBag.SelectedWieght = db.weights.Find(SProd.weight_weight_id).weight_net;
            ViewBag.SelectedStoreId = SProd.store_store_id;
            ViewBag.SelectedStore = db.stores.Find(SProd.store_store_id).storeName;
            ViewBag.SelecteddateId = SProd.store_has_productDate;
            ViewBag.Selecteddate = SProd.store_has_productDate;
            return View(SProd);
        }

        public ActionResult GetProducts()
        {
            var Products = db.SP_Store_To_DataGrid_tmp();
            //var Products1 = db.SP_Store_To_DataGrid_tmp().FirstOrDefault();
            //ViewBag.Shift = Products1.shiftName;
            //ViewBag.Store = Products1.storeName;
            //ViewBag.Product = Products1.productName;
            //ViewBag.Weight = Products1.weight_net;
            //ViewBag.Date = Products1.store_has_productDate;
            return View(Products);
        }

        // GET: StoreProduct/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store_has_product store_has_product = await db.store_has_product.FindAsync(id);
            if (store_has_product == null)
            {
                return HttpNotFound();
            }
            return View(store_has_product);
        }

        // GET: StoreProduct/Create
        public ActionResult Create()
        {
            ViewBag.barcode_serialNumber = new SelectList(db.barcodes, "serialNumber", "serialNumber");
            ViewBag.product_product_id = new SelectList(db.products, "product_id", "productName");
            ViewBag.shift_shift_id = new SelectList(db.shifts, "shift_id", "shiftName");
            ViewBag.store_store_id = new SelectList(db.stores, "store_id", "storeName");
            ViewBag.weight_weight_id = new SelectList(db.weights, "weight_id", "weight_id");
            return View();
        }

        // POST: StoreProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(store_has_product SProd)
        {
            if(SProd!=null)
            {
                ViewBag.SelectedShiftId = SProd.shift_shift_id;
                ViewBag.SelectedProdId = SProd.product_product_id;
                ViewBag.SelectedWieghtId = SProd.weight_weight_id;
                ViewBag.SelectedStoreId = SProd.store_store_id;
                ViewBag.Selecteddate = SProd.store_has_productDate;
            }
            
            if (ModelState.IsValid)
            {

                ObjectParameter RecFound = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter NewIdentity = new ObjectParameter("new_identity", typeof(int));
                try
                {
                    db.SP_Store_Product_Add_New(SProd.shift_shift_id, SProd.product_product_id,
                        SProd.store_store_id, SProd.weight_weight_id, SProd.store_has_productDate,
                        SProd.barcode_serialNumber, NewIdentity,RecFound).ToList();
                    
                    
                    if((int)RecFound.Value > 0)
                    {
                        TempData["Msg"] = "هذه الشكاره تمت تعبئتها من قبل";
                        return RedirectToAction("Index2",SProd);
                    }
                    else if ((int)NewIdentity.Value == 0)
                    {
                        TempData["Msg"] = "هذا السريال غير صحيح";
                        return RedirectToAction("Index2",SProd);
                    }
                    else
                    {
                        TempData["Msg"] = "تمت الاضافه بنجاح";
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index2",SProd);
                    }
                }
                catch
                {
                    return RedirectToAction("Index2",SProd);
                }
                
            }

            ViewBag.barcode_serialNumber = new SelectList(db.barcodes, "serialNumber", "serialNumber");
            ViewBag.product_product_id = new SelectList(db.products, "product_id", "productName");
            ViewBag.shift_shift_id = new SelectList(db.shifts, "shift_id", "shiftName");
            ViewBag.store_store_id = new SelectList(db.stores, "store_id", "storeName");
            ViewBag.weight_weight_id = new SelectList(db.weights, "weight_id", "weight_id");
            return View("Index",SProd);
        }

        // GET: StoreProduct/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store_has_product store_has_product = await db.store_has_product.FindAsync(id);
            if (store_has_product == null)
            {
                return HttpNotFound();
            }
            ViewBag.barcode_serialNumber = new SelectList(db.barcodes, "serialNumber", "serialNumber", store_has_product.barcode_serialNumber);
            ViewBag.product_product_id = new SelectList(db.products, "product_id", "productName", store_has_product.product_product_id);
            ViewBag.shift_shift_id = new SelectList(db.shifts, "shift_id", "shiftName", store_has_product.shift_shift_id);
            ViewBag.store_store_id = new SelectList(db.stores, "store_id", "storeName", store_has_product.store_store_id);
            ViewBag.weight_weight_id = new SelectList(db.weights, "weight_id", "weight_id", store_has_product.weight_weight_id);
            return View(store_has_product);
        }

        // POST: StoreProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "store_store_id,product_product_id,shift_shift_id,barcode_serialNumber,weight_weight_id,store_has_productDate,store_has_product_state")] store_has_product store_has_product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store_has_product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.barcode_serialNumber = new SelectList(db.barcodes, "serialNumber", "serialNumber", store_has_product.barcode_serialNumber);
            ViewBag.product_product_id = new SelectList(db.products, "product_id", "productName", store_has_product.product_product_id);
            ViewBag.shift_shift_id = new SelectList(db.shifts, "shift_id", "shiftName", store_has_product.shift_shift_id);
            ViewBag.store_store_id = new SelectList(db.stores, "store_id", "storeName", store_has_product.store_store_id);
            ViewBag.weight_weight_id = new SelectList(db.weights, "weight_id", "weight_id", store_has_product.weight_weight_id);
            return View(store_has_product);
        }

        // GET: StoreProduct/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store_has_product store_has_product = await db.store_has_product.FindAsync(id);
            if (store_has_product == null)
            {
                return HttpNotFound();
            }
            return View(store_has_product);
        }

        // POST: StoreProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            store_has_product store_has_product = await db.store_has_product.FindAsync(id);
            db.store_has_product.Remove(store_has_product);
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
