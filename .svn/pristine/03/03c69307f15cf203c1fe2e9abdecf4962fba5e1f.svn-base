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
    [Authorize(Roles ="Admin,Operation")]
    public class ItemUnitController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemUnit
        public async Task<ActionResult> Index()
        {
            return View(await db.ItemUnits.ToListAsync());
        }

        // GET: ItemUnit/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemUnit itemUnit = await db.ItemUnits.FindAsync(id);
            if (itemUnit == null)
            {
                return HttpNotFound();
            }
            return View(itemUnit);
        }

        // GET: ItemUnit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemUnit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")] ItemUnit itemUnit)
        {
            if (ModelState.IsValid)
            {
                db.ItemUnits.Add(itemUnit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(itemUnit);
        }

        // GET: ItemUnit/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemUnit itemUnit = await db.ItemUnits.FindAsync(id);
            if (itemUnit == null)
            {
                return HttpNotFound();
            }
            return View(itemUnit);
        }

        // POST: ItemUnit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] ItemUnit itemUnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemUnit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(itemUnit);
        }

        // GET: ItemUnit/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemUnit itemUnit = await db.ItemUnits.FindAsync(id);
            if (itemUnit == null)
            {
                return HttpNotFound();
            }
            return View(itemUnit);
        }

        // POST: ItemUnit/Delete/5
        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var itemUnit = await db.ItemUnits.FindAsync(id);
                db.ItemUnits.Remove(itemUnit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
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
