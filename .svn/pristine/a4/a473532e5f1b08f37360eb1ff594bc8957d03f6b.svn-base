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
    public class BuildingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Building
        public async Task<ActionResult> Index()
        {
            var list = (from D in db.Buildings
                        join C in db.Areas on D.AreaId equals C.Id
                        join A in db.Cities on C.CityId equals A.Id
                        join B in db.Countries on A.CountryId equals B.Id
                        select new BuildingDetails()
                        {
                            Id = D.Id,
                            Name = D.Name,
                            Number = D.Number,
                            AreaId = C.Id,
                            AreaName = C.Name,
                            CityName = A.Name,
                            CountryName = B.Name
                        }).ToList();

            return View( list);
        }

        // GET: Building/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = await db.Buildings.FindAsync(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // GET: Building/Create
        public async Task<ActionResult> Create()
        {
            ViewData["AreasLookup"] = await db.Areas.ToListAsync();
            return View();
        }

        // POST: Building/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Number,AreaId,Latitude,Longitude")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Buildings.Add(building);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(building);
        }

        // GET: Building/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = await db.Buildings.FindAsync(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            ViewData["AreasLookup"] = await db.Areas.ToListAsync();
            return View(building);
        }

        // POST: Building/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Number,AreaId,Latitude,Longitude")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Entry(building).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(building);
        }

        // GET: Building/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = await db.Buildings.FindAsync(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Building/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Building building = await db.Buildings.FindAsync(id);
            db.Buildings.Remove(building);
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
