using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AMS.Context;
using AMS.Models;
using log4net;
using AMS.Helper;

namespace AMS.Controllers
{
    public class AccountStateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: AccountState
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await db.AccountStates.ToListAsync());
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson("");
                Log.Error(ex.Message, ex);
                return View();
            }
        }

        // GET: AccountState/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AccountState accountState = await db.AccountStates.FindAsync(id);
                if (accountState == null)
                {
                    return HttpNotFound();
                }
                return View(accountState);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(id);
                Log.Error(ex.Message, ex);
                return View();
            }
            
        }

        // GET: AccountState/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountState/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,State")] AccountState accountState)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.AccountStates.Add(accountState);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(accountState);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(accountState);
                Log.Error(ex.Message, ex);
                return View();
            }
           
        }

        // GET: AccountState/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountState accountState = await db.AccountStates.FindAsync(id);
            if (accountState == null)
            {
                return HttpNotFound();
            }
            return View(accountState);
        }

        // POST: AccountState/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,State")] AccountState accountState)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(accountState).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(accountState);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(accountState);
                Log.Error(ex.Message, ex);
                return View(accountState);
            }
            
        }

        // GET: AccountState/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountState accountState = await db.AccountStates.FindAsync(id);
            if (accountState == null)
            {
                return HttpNotFound();
            }
            return View(accountState);
        }

        // POST: AccountState/Delete/5
        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                AccountState accountState = await db.AccountStates.FindAsync(id);
                db.AccountStates.Remove(accountState);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(id);
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
