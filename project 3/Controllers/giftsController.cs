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
using System.IO;
using Microsoft.AspNet.Identity;

namespace project_3.Controllers
{
    [Authorize]
    public class giftsController : Controller
    {
        private Entities db = new Entities();

        // GET: gifts
        public async Task<ActionResult> Index()
        {
            return View(await db.gifts.ToListAsync());
        }

        // GET: gifts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gift gift = await db.gifts.FindAsync(id);
            if (gift == null)
            {
                return HttpNotFound();
            }
            return View(gift);
        }

        // GET: gifts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: gifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(gift gift, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image.ContentLength > 0)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(image.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(image.ContentLength);
                    }
                    gift.giftimg = imageData;
                    gift.giftid = 0;
                    gift.userid = User.Identity.GetUserId();
                }
                
                    db.gifts.Add(gift);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(gift);
        }

        // GET: gifts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gift gift = await db.gifts.FindAsync(id);
            if (gift == null)
            {
                return HttpNotFound();
            }
            return View(gift);
        }

        // POST: gifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "userid,giftid,giftname,giftBagsCount,giftimg")] gift gift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gift).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(gift);
        }

        // GET: gifts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gift gift = await db.gifts.FindAsync(id);
            if (gift == null)
            {
                return HttpNotFound();
            }
            return View(gift);
        }

        // POST: gifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            gift gift = await db.gifts.FindAsync(id);
            gift.userid = User.Identity.GetUserId();
            await db.SaveChangesAsync();
            db.gifts.Remove(gift);
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
