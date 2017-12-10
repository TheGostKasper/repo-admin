﻿using System;
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
    public class ItemBrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemBrands
        public async Task<ActionResult> Index()
        {
            return View(await db.ItemBrands.ToListAsync());
        }

        // GET: ItemBrands/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemBrand itemBrand = await db.ItemBrands.FindAsync(id);
            if (itemBrand == null)
            {
                return HttpNotFound();
            }
            return View(itemBrand);
        }

        // GET: ItemBrands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemBrands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")] ItemBrand itemBrand)
        {
            if (ModelState.IsValid)
            {
                //itemBrand.CreationDate = DateTime.UtcNow;
                db.ItemBrands.Add(itemBrand);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(itemBrand);
        }

        // GET: ItemBrands/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemBrand itemBrand = await db.ItemBrands.FindAsync(id);
            if (itemBrand == null)
            {
                return HttpNotFound();
            }
            return View(itemBrand);
        }

        // POST: ItemBrands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] ItemBrand itemBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemBrand).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(itemBrand);
        }

        // GET: ItemBrands/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemBrand itemBrand = await db.ItemBrands.FindAsync(id);
            if (itemBrand == null)
            {
                return HttpNotFound();
            }
            return View(itemBrand);
        }

        // POST: ItemBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ItemBrand itemBrand = await db.ItemBrands.FindAsync(id);
            db.ItemBrands.Remove(itemBrand);
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
