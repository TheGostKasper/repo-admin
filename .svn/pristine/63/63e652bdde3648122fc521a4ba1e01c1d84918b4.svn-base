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
    public class SubCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubCategory
        public async Task<ActionResult> Index()
        {
            var list = (from S in db.SubCategories
                        join C in db.Categories on S.CategoryId equals C.Id
                        select new SubCategoryDetails()
                        {
                            Id = S.Id,
                            NameEN = S.NameEN,
                            NameAR = S.NameAR,
                            CategoryId = S.CategoryId,
                            CategoryName = C.NameEN
                        }).ToListAsync();
            return View(await list);
        }

        // GET: SubCategory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = await db.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // GET: SubCategory/Create
        public async Task<ActionResult> Create()
        {
            ViewData["CategoriesLookup"] = await db.Categories.ToListAsync();
            return View();
        }

        // POST: SubCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NameEN,NameAR,CategoryId")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.SubCategories.Add(subCategory);
                await db.SaveChangesAsync();
                ViewData["CategoriesLookup"] = await db.Categories.ToListAsync();
                return RedirectToAction("Create");
            }

            return View(subCategory);
        }

        // GET: SubCategory/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = await db.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            ViewData["CategoriesLookup"] = await db.Categories.ToListAsync();
            return View(subCategory);
        }

        // POST: SubCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NameEN,NameAR,CategoryId")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subCategory);
        }

        // GET: SubCategory/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = await db.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SubCategory subCategory = await db.SubCategories.FindAsync(id);
            db.SubCategories.Remove(subCategory);
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
