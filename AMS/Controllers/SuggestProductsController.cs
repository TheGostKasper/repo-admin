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

namespace AMS.Controllers
{
    public class SuggestProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SuggestProducts
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: SuggestProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var suggestProduct = GetSuggestProductDetails();
            if (suggestProduct == null)
            {
                return HttpNotFound();
            }
            return View(suggestProduct);
        }

        // GET: SuggestProducts/Create
        public ActionResult Create()
        {
            ViewData["MerchantsLookup"] =  db.Merchants.ToList();
            ViewData["CategoriesLookup"] = db.Categories.ToList();
            return View();
        }

        // POST: SuggestProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserPhone,CategoryId,ProductName,MerchantId")] SuggestProductDetails suggestProduct)
        {
            if (ModelState.IsValid)
            {
                suggestProduct.CreationDate = DateTime.UtcNow;
                suggestProduct.UserId = db.Users.FirstOrDefault(e => e.Mobile == suggestProduct.UserPhone).Id;
                db.SuggestProducts.Add(new SuggestProduct
                {
                    UserId=suggestProduct.UserId,
                    MerchantId=suggestProduct.MerchantId,
                    ProductName=suggestProduct.ProductName,
                    CategoryId=suggestProduct.CategoryId,
                    CreationDate=suggestProduct.CreationDate
                });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(suggestProduct);
        }

        // GET: SuggestProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var suggestProduct = await db.SuggestProducts.FindAsync(id);
            ViewData["UsersLookup"] = await db.Users.ToListAsync();
            ViewData["MerchantsLookup"] = await db.Merchants.ToListAsync();
            ViewData["CategoriesLookup"] = await db.Categories.ToListAsync();
            if (suggestProduct == null)
            {
                return HttpNotFound();
            }
            return View(suggestProduct);
        }

        // POST: SuggestProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserId,CategoryId,ProductName,MerchantId,CreationDate")] SuggestProduct suggestProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suggestProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(suggestProduct);
        }

        // GET: SuggestProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var suggestProduct = GetSuggestProductDetails();
            if (suggestProduct == null)
            {
                return HttpNotFound();
            }
            return View(suggestProduct);
        }

        // POST: SuggestProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SuggestProduct suggestProduct = await db.SuggestProducts.FindAsync(id);
            db.SuggestProducts.Remove(suggestProduct);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public SuggestProductDetails GetSuggestProductDetails()
        {
            return (from sp in db.SuggestProducts
                                  join u in db.Users on sp.UserId equals u.Id
                                  join c in db.Categories on sp.CategoryId equals c.Id
                                  join m in db.Merchants on sp.CategoryId equals m.Id
                                  select new SuggestProductDetails
                                  {
                                      UserId = sp.UserId,
                                      Id = sp.Id,
                                      UserPhone = u.Mobile,
                                      Category = c.NameEN,
                                      CategoryId = sp.CategoryId,
                                      Merchant = m.NameEN,
                                      MerchantId = sp.MerchantId,
                                      ProductName = sp.ProductName,
                                      CreationDate = sp.CreationDate
                                  })
             .FirstOrDefault();
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
