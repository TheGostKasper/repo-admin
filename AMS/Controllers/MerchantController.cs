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
using AMS.Models.Models;
using AMS.Helper;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;
using log4net;

namespace AMS.Controllers
{
    [Authorize]
    public class MerchantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private VXInsertion vx = new VXInsertion();
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Merchant
        public async Task<ActionResult> Index()
        {
            var accState = db.AccountStates.Select(e => new { e.State, e.Id }).AsEnumerable();
            var curr = db.Currencies.Select(e => new { e.Id, e.Name }).AsEnumerable();
            var list = db.Merchants.Select(e => new MerchantDetails
            {
                Id = e.Id,
                NameAR = e.NameAR,
                NameEN = e.NameEN,
                Phone = e.Phone,
                AccountState = accState.FirstOrDefault(ac => ac.Id == e.AccountStateId).State,
                Currency = curr.FirstOrDefault(cu => cu.Id == e.CurrencyId).Name,
                CreationDate = e.CreationDate
            }).ToList();

            return View(list);
        }

        // GET: Merchant/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchant merchant = await db.Merchants.FindAsync(id);
            ViewData["BHLookup"] = await db.BusinessHours.Where(e => e.MerchantId == id).ToListAsync();
            ViewData["BMLookup"] = await db.Merchants.Where(e => e.IsBranch == id).ToListAsync();

            if (merchant == null)
            {
                return HttpNotFound();
            }
            return View(merchant);
        }

        // GET: Merchant/Create
        public async Task<ActionResult> Create()
        {
            ViewData["CountriesLookup"] = await db.Countries.ToListAsync();
            ViewData["AccountStateLookup"] = await db.AccountStates.ToListAsync();
            ViewData["currenciesLookup"] = await db.Currencies.ToListAsync();
            ViewData["LanguageLookup"] = await db.Languages.ToListAsync();
            ViewData["mainSubMLookup"] = await db.Merchants.ToListAsync();
            return View();
        }

        // POST: Merchant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public int Create([Bind(Include = "Id,NameEN,NameAR,Email,Password,SmsCode,Phone,CountryId,CityId,AreaId,Notes,AccountStateId,CurrencyId,LogoUrl,DeliveryTime,ServiceFees,Tax,MinOrder,CreationDate,LangId,Latitude,Longitude,Percentage")] Merchant merchant)
        {
            if (ModelState.IsValid)
            {
                merchant.CreationDate = DateTime.UtcNow;
                merchant.Password = VXSecurity.Encrypt(merchant.Password);
                db.Merchants.Add(merchant);
                var results = db.SaveChanges();
                if (results > 0) return db.Merchants.FirstOrDefault(e=>e.Email==merchant.Email && e.Phone==merchant.Phone).Id;
            }
            return 0;
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public int CreateMerchant(Merchant merchant)
        {
            if (ModelState.IsValid)
            {
                merchant.CreationDate = DateTime.UtcNow;
                merchant.Password = VXSecurity.Encrypt(merchant.Password);
                db.Merchants.Add(merchant);
                var results = db.SaveChanges();
                if (results > 0) return db.Merchants.FirstOrDefault(e => e.Email == merchant.Email && e.Phone == merchant.Phone).Id;
            }
            return 0;
        }



        // GET: Merchant/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchant merchant = await db.Merchants.FindAsync(id);
            merchant.Password = VXSecurity.Decrypt(merchant.Password);
            if (merchant == null)
            {
                return HttpNotFound();
            }
            ViewData["CountriesLookup"] = await db.Countries.ToListAsync();
            ViewData["AccountStateLookup"] = await db.AccountStates.ToListAsync();
            ViewData["currenciesLookup"] = await db.Currencies.ToListAsync();
            ViewData["LanguageLookup"] = await db.Languages.ToListAsync();
            ViewData["mainSubMLookup"] = await db.Merchants.ToListAsync();

            return View(merchant);
        }

        // POST: Merchant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NameEN,NameAR,Email,Password,ShortPassword,SmsCode,Phone,CountryId,Notes,AccountStateId,CurrencyId,LogoUrl,DeliveryTime,ServiceFees,Tax,MinOrder,CreationDate,LangId,Latitude,Longitude,Percentage,IsBranch")] Merchant merchant)
        {
            try
            {
                merchant.Password = VXSecurity.Encrypt(merchant.Password);
                db.Entry(merchant).State = EntityState.Modified;
                db.Entry(merchant).Property(p => p.CreationDate).IsModified = false;
                db.Entry(merchant).Property(p => p.Password).IsModified = false;
                db.Entry(merchant).Property(p => p.SmsCode).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(merchant);
            }

        }

        // GET: Merchant/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchant merchant = await db.Merchants.FindAsync(id);
            merchant.Password = VXSecurity.Decrypt(merchant.Password);
            if (merchant == null)
            {
                return HttpNotFound();
            }
            return View(merchant);
        }

        // POST: Merchant/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var merchant = await db.Merchants.FindAsync(id);
                db.Merchants.Remove(merchant);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpPost]
        public ActionResult GetFile(HttpPostedFileBase file)
        {
            var path= vx.SaveFileToServerPath(file, Server.MapPath("~/UploadFile/"));
            var result=vx.GetListOfTextFile(path);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public static string GetServerUrl()
        {
            if (ConfigurationManager.AppSettings["LiveMode"] == "true")
                return ConfigurationManager.AppSettings["LiveServerUrl"].ToString();
            else
                return ConfigurationManager.AppSettings["TestServerUrl"].ToString();

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