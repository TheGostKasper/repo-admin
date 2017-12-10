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
    public class MerchantServingAreaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MerchantServingArea
        public async Task<ActionResult> Index()
        {
            var list = (from S in db.MerchantServingAreas
                        join C in db.Merchants on S.MerchantId equals C.Id
                        select new MerchantServingAreaDetails()
                        {
                            Id = S.Id,
                            BuildingId = S.BuildingId,
                            MerchantId = S.MerchantId,
                            MerchantName = C.NameEN
                        }).ToListAsync();
            return View(await list);
        }

        // GET: MerchantServingArea/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantServingArea merchantServingArea = await db.MerchantServingAreas.FindAsync(id);
            if (merchantServingArea == null)
            {
                return HttpNotFound();
            }
            return View(merchantServingArea);
        }

        // GET: MerchantServingArea/Create
        public async Task<ActionResult> Create()
        {
            ViewData["CountriesLookup"] = await db.Countries.ToListAsync();
            ViewData["MerchantsLookup"] = await db.Merchants.ToListAsync();
            return View();
        }

        // POST: MerchantServingArea/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,BuildingId,MerchantId")] MerchantServingArea merchantServingArea)
        {
            if (ModelState.IsValid)
            {
                db.MerchantServingAreas.Add(merchantServingArea);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(merchantServingArea);
        }
        [HttpPost]
        public async Task<ActionResult> Create2(MerchantServingArea merchantServingArea)
        {
            if (ModelState.IsValid)
            {
                db.MerchantServingAreas.Add(merchantServingArea);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(merchantServingArea);
        }
        // GET: MerchantServingArea/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantServingArea merchantServingArea = await db.MerchantServingAreas.FindAsync(id);
            if (merchantServingArea == null)
            {
                return HttpNotFound();
            }
            ViewData["CountriesLookup"] = await db.Countries.ToListAsync();
           // ViewData["CitiesLookup"] = await db.Cities.ToListAsync();
           // ViewData["AreasLookup"] = await db.Areas.ToListAsync();
           // ViewData["BuildingsLookup"] = await db.Buildings.ToListAsync();
            ViewData["MerchantsLookup"] = await db.Merchants.ToListAsync();

            return View(merchantServingArea);
        }

        // POST: MerchantServingArea/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,BuildingId,MerchantId")] MerchantServingArea merchantServingArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchantServingArea).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(merchantServingArea);
        }

        [HttpPost]
        public async Task<ActionResult> Edit2(MerchantServingArea merchantServingArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchantServingArea).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(merchantServingArea);
        }
        // GET: MerchantServingArea/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantServingArea merchantServingArea = await db.MerchantServingAreas.FindAsync(id);
            if (merchantServingArea == null)
            {
                return HttpNotFound();
            }
            return View(merchantServingArea);
        }

        // POST: MerchantServingArea/Delete/5
        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MerchantServingArea merchantServingArea = await db.MerchantServingAreas.FindAsync(id);
            db.MerchantServingAreas.Remove(merchantServingArea);
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
