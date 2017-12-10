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
    public class ItemPackagingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemPackagings
        public async Task<ActionResult> Index()
        {
            return View(await db.ItemPackagings.ToListAsync());
        }

        // GET: ItemPackagings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemPackaging itemPackaging = await db.ItemPackagings.FindAsync(id);
            if (itemPackaging == null)
            {
                return HttpNotFound();
            }
            return View(itemPackaging);
        }

        // GET: ItemPackagings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemPackagings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")] ItemPackaging itemPackaging)
        {
            if (ModelState.IsValid)
            {
                db.ItemPackagings.Add(itemPackaging);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(itemPackaging);
        }

        // GET: ItemPackagings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemPackaging itemPackaging = await db.ItemPackagings.FindAsync(id);
            if (itemPackaging == null)
            {
                return HttpNotFound();
            }
            return View(itemPackaging);
        }

        // POST: ItemPackagings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] ItemPackaging itemPackaging)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemPackaging).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(itemPackaging);
        }

        // GET: ItemPackagings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemPackaging itemPackaging = await db.ItemPackagings.FindAsync(id);
            if (itemPackaging == null)
            {
                return HttpNotFound();
            }
            return View(itemPackaging);
        }

        // POST: ItemPackagings/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ItemPackaging itemPackaging = await db.ItemPackagings.FindAsync(id);
            db.ItemPackagings.Remove(itemPackaging);
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