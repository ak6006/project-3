﻿using System;
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
using Microsoft.AspNet.Identity;

namespace project_3.Controllers
{
    [Authorize]
    public class productsController : Controller
    {
        private Entities db = new Entities();

        // GET: products
        public ActionResult Index()
        {
            var Products = db.products.ToList();
            return View(Products);
        }
        public ActionResult Search(string Key)
        {
            if (Key != null)
            {
                var products = db.SP_Product_Search(Key).ToList();
                //TempData["SearchKey"] = Key;
                return View(products);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = await db.products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(product product)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter rec_found = new ObjectParameter("rec_found", typeof(int));
                ObjectParameter new_identity = new ObjectParameter("new_identity", typeof(int));
                db.SP_Product_Add_New(User.Identity.GetUserId(),product.productName,product.TodayPrice, new_identity, rec_found).ToList();

                if ((int)rec_found.Value == 0)
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
            return View(product);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.products.Find(id);
            
            
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(product product)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter rec_found = new ObjectParameter("rec_found", typeof(int));
                db.SP_Update_Product_Price(User.Identity.GetUserId(),product.product_id, product.productName,product.TodayPrice);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.SP_Product_ID(id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var product = db.SP_Product_ID(id).FirstOrDefault();
            try
            {
                db.SP_Product_DELETE(User.Identity.GetUserId(),product.رقم_المنتج);
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
