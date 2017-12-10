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
using AMS.Models;

namespace AMS.Controllers
{
    public class MerchantCouponTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MerchantCouponTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.MerchantCouponTypes.ToListAsync());
        }

        // GET: MerchantCouponTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantCouponType merchantCouponType = await db.MerchantCouponTypes.FindAsync(id);
            if (merchantCouponType == null)
            {
                return HttpNotFound();
            }
            return View(merchantCouponType);
        }

        // GET: MerchantCouponTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MerchantCouponTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,CreationDate")] MerchantCouponType merchantCouponType)
        {
            if (ModelState.IsValid)
            {
                db.MerchantCouponTypes.Add(merchantCouponType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(merchantCouponType);
        }

        // GET: MerchantCouponTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantCouponType merchantCouponType = await db.MerchantCouponTypes.FindAsync(id);
            if (merchantCouponType == null)
            {
                return HttpNotFound();
            }
            return View(merchantCouponType);
        }

        // POST: MerchantCouponTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,CreationDate")] MerchantCouponType merchantCouponType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchantCouponType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(merchantCouponType);
        }

        // GET: MerchantCouponTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantCouponType merchantCouponType = await db.MerchantCouponTypes.FindAsync(id);
            if (merchantCouponType == null)
            {
                return HttpNotFound();
            }
            return View(merchantCouponType);
        }

        // POST: MerchantCouponTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var merchantCouponType =  db.MerchantCouponTypes.Find(id);
                db.MerchantCouponTypes.Remove(merchantCouponType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
            
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
