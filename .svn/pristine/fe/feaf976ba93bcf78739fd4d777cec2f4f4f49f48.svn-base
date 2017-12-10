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
using AMS.Models.Models;
using AMS.Models;

namespace AMS.Controllers
{
    public class ReportingIssuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReportingIssues
        public async Task<ActionResult> Index()
        {
            //await db.ReportingIssues.ToListAsync()
            return View();
        }

        // GET: ReportingIssues/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reportingIssues = await db.ReportingIssues.FindAsync(id);
            if (reportingIssues == null)
            {
                return HttpNotFound();
            }
            return View(reportingIssues);
        }

        // GET: ReportingIssues/Create
        public ActionResult Create()
        {

            ViewData["MerchantsLookup"] = db.Merchants.ToList();
            ViewData["ACsLookup"] = db.AccountStates.ToList();
            return View();
        }

        // POST: ReportingIssues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,merchantId,ShortOrderNumber,Text,Status,CreationDate")] ReportingIssuesDetails reportingIssues)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //reportingIssues.CreationDate = DateTime.UtcNow;
                    var ord = db.Orders.FirstOrDefault(e => e.MerchantId == reportingIssues.merchantId &&
                    e.ShortOrderNumber == reportingIssues.OrderNumber).Id;
                   
                        db.ReportingIssues.Add(new ReportingIssues
                        {
                            CreationDate = DateTime.UtcNow,
                            merchantId = reportingIssues.merchantId,
                            OrderId = ord,
                            StatusId = reportingIssues.StatusId,
                            Text = reportingIssues.Text
                        });
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //return RedirectToAction("Create");
                    throw;
                }

            }

            return View(reportingIssues);
        }

        // GET: ReportingIssues/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var reportingIssues = await db.ReportingIssues.FindAsync(id);
                if (reportingIssues == null)
                {
                    return HttpNotFound();
                }
                return View(reportingIssues);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // POST: ReportingIssues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,merchantId,OrderId,Text,Status,CreationDate")] ReportingIssues reportingIssues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportingIssues).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reportingIssues);
        }

        // GET: ReportingIssues/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reportingIssues = await db.ReportingIssues.FindAsync(id);
            if (reportingIssues == null)
            {
                return HttpNotFound();
            }
            return View(reportingIssues);
        }

        // POST: ReportingIssues/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var reportingIssues = await db.ReportingIssues.FindAsync(id);
            db.ReportingIssues.Remove(reportingIssues);
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
