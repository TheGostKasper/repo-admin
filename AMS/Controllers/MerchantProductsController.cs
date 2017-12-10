using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMS.Context;
using AMS.Models.Models;

namespace AMS.Controllers
{
    [Authorize]
    public class MerchantProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MerchantProducts
        public async Task<ActionResult> Index()
        {
            return View(await db.MerchantProducts.ToListAsync());
        }

        // GET: item/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: MerchantProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MerchantProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MerchantId,ItemId,Price,Quantity,SubCategoryId,CreationDate")] MerchantProduct merchantProduct)
        {
            if (ModelState.IsValid)
            {
                db.MerchantProducts.Add(merchantProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(merchantProduct);
        }

        // GET: MerchantProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantProduct merchantProduct = await db.MerchantProducts.FindAsync(id);
            if (merchantProduct == null)
            {
                return HttpNotFound();
            }
            return View(merchantProduct);
        }

        // POST: MerchantProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MerchantId,ItemId,Price,Quantity,SubCategoryId,CreationDate")] MerchantProduct merchantProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchantProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(merchantProduct);
        }

        // GET: MerchantProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantProduct merchantProduct = await db.MerchantProducts.FindAsync(id);
            if (merchantProduct == null)
            {
                return HttpNotFound();
            }
            return View(merchantProduct);
        }

        // POST: MerchantProducts/Delete/5
        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MerchantProduct merchantProduct = await db.MerchantProducts.FindAsync(id);
            db.MerchantProducts.Remove(merchantProduct);
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
