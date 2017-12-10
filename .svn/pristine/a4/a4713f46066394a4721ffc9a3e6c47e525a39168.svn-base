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
using log4net;
using AMS.Helper;

namespace AMS.Controllers
{
    [Authorize]
    public class BusinessHoursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: BusinessHours
        public async Task<ActionResult> Index()
        {
            try
            {
                var results = new List<BHDetails>();
                var items = await db.BusinessHours.ToListAsync();
                var merchants = db.Merchants.Select(e => new { Id = e.Id, NameEN = e.NameEN }).ToList();
                foreach (var item in items)
                {
                    var merchName = merchants.FirstOrDefault(m => m.Id == item.MerchantId);
                    if (merchName != null)
                    {
                        results.Add(new BHDetails
                        {
                            OpenTime = item.OpenTime,
                            CloseTime = item.CloseTime,
                            Day = ((Days)item.Day).ToString(),
                            MerchantId = item.MerchantId,
                            Id = item.Id,
                            MerchantName = merchants.FirstOrDefault(m => m.Id == item.MerchantId).NameEN
                        });
                    }
                }

                return View(results);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { });
                Log.Error(ex.Message, ex);
                return View();
            }

        }

        // GET: BusinessHours/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ViewData["MerchantsLookup"] = await db.Merchants.ToListAsync();

                var businessHours = await db.BusinessHours.FindAsync(id);
                if (businessHours == null)
                {
                    return HttpNotFound();
                }
                return View(businessHours);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { Id = id });
                Log.Error(ex.Message, ex);
                throw;
            }

        }

        // GET: BusinessHours/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                ViewData["MerchantsLookup"] = await db.Merchants.ToListAsync();
                return View();
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { MerchantsLookup = "" });
                Log.Error(ex.Message, ex);
                return View();
            }

        }

        // POST: BusinessHours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more de  tails see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MerchantId,Day,OpenTime,CloseTime")] BusinessHours businessHours)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bh = db.BusinessHours.ToList();
                    var bhMerch = bh.FirstOrDefault(e => e.MerchantId == businessHours.MerchantId && e.Day == businessHours.Day);
                    if (bhMerch==null)
                    {
                        db.BusinessHours.Add(businessHours);
                        await db.SaveChangesAsync();
                    }else
                    {
                        bhMerch.OpenTime = businessHours.OpenTime;
                        bhMerch.CloseTime = businessHours.CloseTime;
                         await db.SaveChangesAsync();
                    }
                    return Json(businessHours);
                }
                return Json(businessHours);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(businessHours);
                Log.Error(ex.Message, ex);
                return View(businessHours);
            }
        }

        // GET: BusinessHours/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var businessHours = await db.BusinessHours.FindAsync(id);
                if (businessHours == null)
                {
                    return HttpNotFound();
                }
                ViewData["MerchantsLookup"] = await db.Merchants.ToListAsync();
                return View(businessHours);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { Id = id });
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }

        }

        // POST: BusinessHours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MerchantId,Day,OpenTime,CloseTime")] BusinessHours businessHours)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(businessHours).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(businessHours);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(businessHours);
                Log.Error(ex.Message, ex);
                return View(businessHours);
            }

        }

        // GET: BusinessHours/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var businessHours = await db.BusinessHours.FindAsync(id);
            if (businessHours == null)
            {
                return HttpNotFound();
            }
            return View(businessHours);
        }

        // POST: BusinessHours/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var businessHours = await db.BusinessHours.FindAsync(id);
                db.BusinessHours.Remove(businessHours);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { Id = id });
                Log.Error(ex.Message, ex);
                return View();
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
