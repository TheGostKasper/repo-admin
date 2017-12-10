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
using AMS.Models;

namespace AMS.Controllers
{
    [Authorize]
    public class MerchantCourierController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MerchantCourier
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: MerchantCourier/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courier courier = await db.Couriers.FindAsync(id);
            if (courier == null)
            {
                return HttpNotFound();
            }
            return View(courier);
        }

        // GET: MerchantCourier/Create
        public async Task<ActionResult> Create()
        {
            ViewData["MerchantsLookup"] = await db.Merchants.ToListAsync();
            return View();
        }

        // POST: MerchantCourier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Mobile,MerchantId")] Courier courier)
        {
            try
            {
                db.Couriers.Add(courier);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View(courier);
            }
                
        }

        // GET: MerchantCourier/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courier courier = await db.Couriers.FindAsync(id);
            if (courier == null)
            {
                return HttpNotFound();
            }
            ViewData["MerchantsLookup"] = await db.Merchants.ToListAsync();
            return View(courier);
        }

        // POST: MerchantCourier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Mobile,MerchantId")] Courier courier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courier).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(courier);
        }

        // GET: MerchantCourier/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courier courier = await db.Couriers.FindAsync(id);
            if (courier == null)
            {
                return HttpNotFound();
            }
            return View(courier);
        }

        // POST: MerchantCourier/Delete/5
        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Courier courier = await db.Couriers.FindAsync(id);
            db.Couriers.Remove(courier);
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