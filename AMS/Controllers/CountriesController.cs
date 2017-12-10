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
    public class CountriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: Countries
        public async Task<ActionResult> Index()
        {
            try
            {
                var result = await db.Countries.ToListAsync();
                return View(result);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { });
                Log.Error(ex.Message, ex);
                return View();
            }
           
        }

        // GET: Countries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var country = await db.Countries.FindAsync(id);
                if (country == null)
                {
                    return HttpNotFound();
                }
                return View(country);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { Details="Object Details not found " ,ID=id });
                Log.Error(ex.Message, ex);
                return RedirectToAction("Index");
            }
           
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,NameAR")] Country country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Countries.Add(country);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(country);
                Log.Error(ex.Message, ex);
                return View(country);
            }
           
        }

        // GET: Countries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var country = await db.Countries.FindAsync(id);
                if (country == null)
                {
                    return HttpNotFound();
                }
                return View(country);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { Details = "Object not found ", ID = id });
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
           
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,NameAR")] Country country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(country).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(country);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(country);
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
            
        }

        // GET: Countries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var country = await db.Countries.FindAsync(id);
                if (country == null)
                {
                    return HttpNotFound();
                }
                return View(country);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { Details = "Object not found ", ID = id });
                Log.Error(ex.Message, ex);
                return RedirectToAction("Index");
            }
           
        }

        // POST: Countries/Delete/5
        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Country country = await db.Countries.FindAsync(id);
                db.Countries.Remove(country);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { Details = "Object not found ", ID = id });
                Log.Error(ex.Message, ex);
                return RedirectToAction("Delete",new { id=id});
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
