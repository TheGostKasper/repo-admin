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
using AMS.Models;

namespace AMS.Controllers
{
    [Authorize]
    public class MerchantSupervisorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MerchantSupervisor
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: MerchantSupervisor/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantSupervisor merchantSupervisor = await db.MerchantSupervisors.FindAsync(id);
            if (merchantSupervisor == null)
            {
                return HttpNotFound();
            }
            return View(merchantSupervisor);
        }

        // GET: MerchantSupervisor/Create
        public async Task<ActionResult> Create()
        {
            ViewData["MerchantsLookup"] = await db.Merchants.ToListAsync();
            return View();
        }

        // POST: MerchantSupervisor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Mobile,MerchantId")] MerchantSupervisor merchantSupervisor)
        {
            if (ModelState.IsValid)
            {
                db.MerchantSupervisors.Add(merchantSupervisor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(merchantSupervisor);
        }

        // GET: MerchantSupervisor/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantSupervisor merchantSupervisor = await db.MerchantSupervisors.FindAsync(id);
            if (merchantSupervisor == null)
            {
                return HttpNotFound();
            }
            ViewData["MerchantsLookup"] = await db.Merchants.ToListAsync();
            return View(merchantSupervisor);
        }

        // POST: MerchantSupervisor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Mobile,MerchantId")] MerchantSupervisor merchantSupervisor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchantSupervisor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(merchantSupervisor);
        }

        // GET: MerchantSupervisor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantSupervisor merchantSupervisor = await db.MerchantSupervisors.FindAsync(id);
            if (merchantSupervisor == null)
            {
                return HttpNotFound();
            }
            return View(merchantSupervisor);
        }

        // POST: MerchantSupervisor/Delete/5
        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MerchantSupervisor merchantSupervisor = await db.MerchantSupervisors.FindAsync(id);
            db.MerchantSupervisors.Remove(merchantSupervisor);
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
