using AMS.Context;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;
using AMS.Helper;
using System.Web.Script.Serialization;
using System;
using AMS.Models;
using System.Data.Entity.Validation;
using log4net;

namespace AMS.Controllers
{
    [Authorize]
    public class MerchantProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private MerchantOperation _op;
        private AMS.Helper.ExcelEntry _excelEntry;
        public static int _merchant = 0;

        public MerchantProductController()
        {
            _excelEntry = new Helper.ExcelEntry();
            _op = new MerchantOperation();

        }
        // GET: MerchantProduct
        public ActionResult Index()
        {
            var items = new List<MerchantDetails>();
            using (var _db = new ApplicationDbContext())
            {
                items = _db.Merchants.Select(e => new MerchantDetails
                {
                    Id = e.Id,
                    NameEN = e.NameEN,
                    NameAR = e.NameAR
                }).ToList();
            }


            var listmerchant = new List<SelectListItem>();
            foreach (var item in items)
            {
                listmerchant.Add(new SelectListItem { Text = item.NameEN, Value = item.Id.ToString() });
            }
            // ViewBag["listOfMerchants"] = listmerchant;
            return View(listmerchant);
        }


        [HttpGet]
        public int Merchant(int id)
        {
            _merchant = id;
            return id;
        }

        [HttpPost]
        public string Upload(HttpPostedFileBase file)
        {
            try
            {

                if (file != null && file.ContentLength > 0)
                {
                    var path = Server.MapPath("~/Excel_Files/") + file.FileName;

                    using (var fileStream = System.IO.File.Create(path))
                    {
                        file.InputStream.Seek(0, SeekOrigin.Begin);
                        file.InputStream.CopyTo(fileStream);
                    }

                    return path;
                }
                else
                {
                    return "";
                }
            }
            catch (System.Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(file);
                Log.Error(ex.Message, ex);
                return "";
            }


        }


        [HttpGet]
        public void SaveToken(string token)
        {
            _op.SaveToken(token, Server.MapPath("~/Log/weelo-merchantProduct.txt"));
        }
        [HttpPost]
        public ActionResult GetItems(MerchantRequest mRequest)
        {
            try
            {
                var result= _op.GetListItem(mRequest.Path);
                var results = result.Skip((mRequest.Page - 1) * mRequest.PageSize).Take(mRequest.PageSize).ToList();
                var jsonObj = new JavaScriptSerializer().Serialize(new MItemPaging<MItemDetails>
                {
                    Total = result.Count,
                    Data = results
                });
                _op.WriteToFile(new List<string> { string.Format("Data fetched success, Total Data= {0} , page={1}", results.Count,mRequest.Page) });
                return Json(jsonObj);
            }
            catch (Exception ex)
            {
                _op.WriteToFile(new List<string> { ex.Message });
                return null;
            }
            
        }

        [HttpPost]
        public ActionResult GetMatchedItems(MerchantRequest mRequest)
        {
            try
            {
                var result = _op.GetMatchedItems(mRequest.Path);
                var results = result.Skip((mRequest.Page - 1) * mRequest.PageSize).Take(mRequest.PageSize).ToList();
                var jsonObj = new JavaScriptSerializer().Serialize(new MItemPaging<MItemDetails>
                {
                    Total = result.Count,
                    Data = results
                });
                _op.WriteToFile(new List<string> { string.Format("Matched products, Total Data= {0} ", result.Count) });
                return Json(jsonObj);
            }
            catch (Exception ex)
            {
                _op.WriteToFile(new List<string> { ex.Message });
                return null;
            }

        }

        [HttpPost]
        public ActionResult GetUnMatchedItems(MerchantRequest mRequest)
        {
            try
            {
                var result = _op.GetUnMatchedItems(mRequest.Path);
                var results = result.Skip((mRequest.Page - 1) * mRequest.PageSize).Take(mRequest.PageSize).ToList();
                var jsonObj = new JavaScriptSerializer().Serialize(new MItemPaging<MItemDetails>
                {
                    Total = result.Count,
                    Data = results
                });
                _op.WriteToFile(new List<string> { string.Format("UnMateched products, Total Data= {0}", result.Count) });
                return Json(jsonObj);
            }
            catch (Exception ex)
            {
                _op.WriteToFile(new List<string> { ex.Message });
                return null;
            }

        }

        [HttpPost]
        public ActionResult ReadExcelDatatable(MerchantRequest mRequest)
        {
            try
            {
                var result = _excelEntry.ReadFromExcel(mRequest.Path);
                //InsertItemsToDb(result);
                var results = result.Skip((mRequest.Page - 1) * mRequest.PageSize).Take(mRequest.PageSize).ToList();
                var jsonObj = new JavaScriptSerializer().Serialize(new MerchantProductPaging
                {
                    Total = result.Count,
                    Data = results
                });


                return Json(jsonObj);
            }
            catch (System.Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(mRequest);
                Log.Error(ex.Message, ex);
                return null;
            }
        }
        public void InsertItemsToDb(HashSet<MerchantProductDetails> items)
        {
            //var items = _excelEntry.ReadFromExcel(path);
            try
            {
                var dbItems = db.Items.ToList();
                foreach (var item in items)
                {
                    var itm = dbItems.FirstOrDefault(e => e.Barcode == item.Barcode);
                    if (itm == null)
                    {
                        var itmTA = new Item
                        {
                            Barcode = (item.Barcode.Length < 12) ? GenerateBarcode() : item.Barcode,
                            CreationDate = DateTime.UtcNow,
                            ImageUrl = "default",
                            NameAR = (item.NameAR != "") ? item.NameAR : "--",
                            NameEN = (item.NameEN != "") ? item.NameEN : "--",
                            Volume = (item.Volume != "") ? item.Volume : "1 pack",
                            UnitId = 1,
                            SubCategoryId = 47
                        };
                        db.Items.Add(itmTA);
                        db.SaveChanges();
                        dbItems.Add(itmTA);
                    }

                }


            }
            catch (DbEntityValidationException ex)
            {
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(ex);
                Log.Error(ex.Message, ex);
            }



        }

        public string GenerateBarcode()
        {
            //return System.Guid.NewGuid().ToString().Substring(0, 12);
            var rnd = new Random();
            var value = rnd.Next(0, 9999999).ToString("D12");
            return value;
            // var rnd = new Random(13);
            //int value= rnd.Next(0, 9);
            // return value.ToString();
        }
        [HttpPost]
        public ActionResult ReadExcelData(string path)
        {
            try
            {
                var result = _excelEntry.ReadFromExcel(path);
                return Json(result);
            }
            catch (System.Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(path);
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult GetProductStatus(string path)
        {
            try
            {
                var _results = _excelEntry.CompareFiles(path, "match");
                var jsonObj = new JavaScriptSerializer().Serialize(new MerchantProductPaging
                {
                    MatchedProductTotal = _results.MatchedProduct.Count,
                    MatchedProduct = _results.MatchedProduct
                });
                return Json(jsonObj);
            }
            catch (System.Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(path);
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult GetProductUnMatchedStatus(string path)
        {
            try
            {
                var _results = _excelEntry.CompareFiles(path, "unmatch");
                var jsonObj = new JavaScriptSerializer().Serialize(new MerchantProductPaging
                {
                    UnmatchedProductTotal = _results.UnmatchedProduct.Count,
                    UnmatchedProduct = _results.UnmatchedProduct
                });
                return Json(jsonObj);
            }
            catch (System.Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(path);
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult GetProductStatusByNames(string path)
        {
            try
            {
                var _results = _excelEntry.CompareFilesByItemsNames(path, "match");
                var jsonObj = new JavaScriptSerializer().Serialize(new MerchantProductPaging
                {
                    MatchedProductTotal = _results.MatchedProduct.Count,
                    MatchedProduct = _results.MatchedProduct
                });
                return Json(jsonObj);
            }
            catch (System.Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(path);
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult GetProductUnMatchedStatusByNames(string path)
        {
            try
            {
                var _results = _excelEntry.CompareFilesByItemsNames(path, "unmatch");
                var jsonObj = new JavaScriptSerializer().Serialize(new MerchantProductPaging
                {
                    UnmatchedProductTotal = _results.UnmatchedProduct.Count,
                    UnmatchedProduct = _results.UnmatchedProduct
                });
                return Json(jsonObj);
            }
            catch (System.Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(path);
                Log.Error(ex.Message, ex);
                return null;
            }
        }


        public List<ProductDetails> GetDBProduct()
        {
            try
            {
                using (var _db = new ApplicationDbContext())
                {
                    var products = _db.Items.Select(e => new ProductDetails
                    {
                        Barcode = e.Barcode,
                        Id = e.Id
                    }).Where(e => e.Barcode != null).ToList();
                    return products;
                }
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { });
                Log.Error(ex.Message, ex);
                return null;
            }

        }
        [HttpPost]
        public void InsertData(MerchantComparedProducts _merchantComparedProducts)
        {
            try
            {
                var mrcId = _merchantComparedProducts.MerchantId;
                var mrchProds = db.MerchantProducts.Where(p => p.MerchantId == _merchantComparedProducts.MerchantId).ToList();
                var catIds = db.Categories.Select(e => new { Id = e.Id }).ToList();
                var mrchCats = db.MerchantCategories.Select(e => new { CategoryId = e.CategoryId, e.MerchantId }).ToList();
                var mrchSCats = db.MerchantSubCategories.Select(e => new { SubCategoryId = e.SubCategoryId, CategoryId = e.CategoryId, e.MerchantId }).ToList();

                foreach (var item in _merchantComparedProducts.MatchedProduct)
                {
                    var prod = mrchProds.FirstOrDefault(ItemId => ItemId.ItemId == item.ItemId);
                    if (prod == null)
                    {
                        var mPROD = new MerchantProduct
                        {
                            ItemId = item.ItemId,
                            MerchantId = mrcId,
                            Price = item.Price,
                            Quantity = Convert.ToInt32(item.Quantity),
                            SubCategoryId = Convert.ToInt32(item.SubCategoryId),
                            CreationDate = DateTime.UtcNow
                        };
                        db.MerchantProducts.Add(mPROD);
                        

                        var pSub = mrchSCats.FirstOrDefault(e => e.SubCategoryId == Convert.ToInt32(item.SubCategoryId) &&
                        e.MerchantId == mrcId);
                        
                        if (pSub == null)
                        {
                            db.MerchantCategories.Add(new MerchantCategories
                            {
                                CategoryId = pSub.CategoryId,
                                MerchantId = mrcId,
                                CreationDate = DateTime.UtcNow
                            });

                            db.MerchantSubCategories.Add(new MerchantSubCategory
                            {
                                MerchantId = mrcId,
                                SubCategoryId = item.SubCategoryId,
                                CreationDate = DateTime.UtcNow,
                                CategoryId = pSub.CategoryId
                            });
                        }

                        db.SaveChanges();
                    }
                }
            }
            catch (System.Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(_merchantComparedProducts);
                Log.Error(ex.Message, ex);
            }

        }
        [HttpPost]
        public void InsertUnMatechedData(MerchantComparedProducts _merchantComparedProducts)
        {
            try
            {
                var itm = db.Items.ToList();

                foreach (var item in _merchantComparedProducts.UnmatchedProduct)
                {
                    var prod = itm.FirstOrDefault(e => e.Barcode == item.Barcode);

                    if (prod == null)
                    {
                        var mPROD = new Item
                        {
                            NameAR = item.NameAR,
                            NameEN = "--",
                            Barcode = item.Barcode,
                            SubCategoryId = 1,
                            ImageUrl = "",
                            Volume = "1",
                            UnitId = 1,
                            CreationDate = DateTime.UtcNow
                        };
                        db.Items.Add(mPROD);
                        db.SaveChanges();
                        itm.Add(mPROD);
                    }

                }
            }
            catch (System.Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(_merchantComparedProducts);
                Log.Error(ex.Message, ex);
            }
        }



        // GET: MerchantProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantProduct merchantProduct = await db.MerchantProducts.FindAsync(id);
            if (merchantProduct == null)
            {
                return HttpNotFound();
            }
            return View(merchantProduct);
        }

        // GET: MerchantProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MerchantProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MerchantId,ItemId,Price,Quantity,SubCategoryId,CreationDate")] MerchantProduct merchantProduct)
        {
            if (ModelState.IsValid)
            {
                db.MerchantProducts.Add(merchantProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(merchantProduct);
        }

        // GET: MerchantProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantProduct merchantProduct = await db.MerchantProducts.FindAsync(id);
            if (merchantProduct == null)
            {
                return HttpNotFound();
            }
            return View(merchantProduct);
        }

        // POST: MerchantProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MerchantId,ItemId,Price,Quantity,SubCategoryId,CreationDate")] MerchantProduct merchantProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchantProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(merchantProduct);
        }

        // GET: MerchantProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantProduct merchantProduct = await db.MerchantProducts.FindAsync(id);
            if (merchantProduct == null)
            {
                return HttpNotFound();
            }
            return View(merchantProduct);
        }

        // POST: MerchantProducts/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MerchantProduct merchantProduct = await db.MerchantProducts.FindAsync(id);
            db.MerchantProducts.Remove(merchantProduct);
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