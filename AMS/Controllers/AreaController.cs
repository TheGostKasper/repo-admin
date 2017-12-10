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
    public class AreaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Areas
        public async Task<ActionResult> Index()
        {
            var list = (from C in db.Areas
                        join A in db.Cities on C.CityId equals A.Id
                        join B in db.Countries on A.CountryId equals B.Id
                        select new AreaDetails()
                        {
                            Id = C.Id,
                            Name = C.Name,
                            CityId = C.CityId,
                            CityName = A.Name,
                            CountryName = B.Name
                        }).ToListAsync();

            return View(await list);
        }

        // GET: Areas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = await db.Areas.FindAsync(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // GET: Areas/Create
        public async Task<ActionResult> Create()
        {
            ViewData["CitiesLookup"] = await db.Cities.ToListAsync();
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CityId")] Area area)
        {
            if (ModelState.IsValid)
            {
                db.Areas.Add(area);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(area);
        }

        // GET: Areas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = await db.Areas.FindAsync(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            ViewData["CitiesLookup"] = await db.Cities.ToListAsync();
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CityId")] Area area)
        {
            if (ModelState.IsValid)
            {
                db.Entry(area).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(area);
        }

        // GET: Areas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = await db.Areas.FindAsync(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Area area = await db.Areas.FindAsync(id);
            db.Areas.Remove(area);
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
