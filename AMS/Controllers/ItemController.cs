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
    [Authorize(Roles = "Admin,Operation")]
    public class ItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Item
        public async Task<ActionResult> Index()
        {
            return View();
        }



        // GET: Item/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item =await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Item/Create
        public async Task<ActionResult> Create()
        {
            ViewData["SubCategoriesLookup"] = await db.SubCategories.ToListAsync();
            ViewData["ItemUnitsLookup"] = await db.ItemUnits.ToListAsync();
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NameEN,NameAR,SubCategoryId,Volume,UnitId,ImageUrl,Barcode,CreationDate")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.CreationDate = DateTime.UtcNow;
                db.Items.Add(item);
                await db.SaveChangesAsync();
                //ViewData["SubCategoriesLookup"] = await db.SubCategories.ToListAsync();
                //ViewData["ItemUnitsLookup"] = await db.ItemUnits.ToListAsync();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var _db=new ApplicationDbContext())
            {
                var item = _db.Items.FirstOrDefault(e=>e.Id==id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                ViewData["SubCategoriesLookup"] =  _db.SubCategories.ToList();
                ViewData["ItemUnitsLookup"] =  _db.ItemUnits.ToList();

                return View(item);
            }
          
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NameEN,NameAR,SubCategoryId,Volume,UnitId,ImageUrl,Barcode,CreationDate")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.Entry(item).Property(p => p.CreationDate).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Item/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Item/Delete/5
        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Item item = await db.Items.FindAsync(id);
            db.Items.Remove(item);
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
