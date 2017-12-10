using AMS.Models;
using System;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using AMS.Context;
using AMS.Models.Models;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Data.Entity.Validation;

namespace AMS.Helper
{
    public class ExcelEntry
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ExcelEntry()
        {
        }
        public void CreateExcel<T>(string path, string sheetName, IEnumerable<T> data)
        {
            try
            {
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add(sheetName);

                var header = new ExcelReorder().ToString().Split(',').ToList();
                var dataCount = header.Count;

                for (int i = 0; i < dataCount; i++)
                {
                    ws.Cell(1, i + 1).InsertData(header); //insert titles to first row
                    header.RemoveAt(0);
                }

                if (data != null && data.Count() > 0)
                {
                    ws.Cell(2, 1).InsertData(data);
                }
                wb.SaveAs(path);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public HashSet<MerchantProductDetails> ReadFromExcel(string path)
        {
            var products = new HashSet<MerchantProductDetails>(new CompareProduct());
            try
            {
                string constr = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES""", path.Replace(@"\\", @"\"));
                ;
                using (OleDbConnection conn = new OleDbConnection(constr))
                {
                    conn.Open();
                    System.Data.DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    foreach (System.Data.DataRow dr in Sheets.Rows)
                    {
                        string sht = dr[2].ToString().Replace("'", "");
                        OleDbCommand command = new OleDbCommand("select * from [" + sht + "]", conn);
                        OleDbDataReader reader = command.ExecuteReader();
                        products = AddProducts(reader);
                    }
                }

                return products;
            }
            catch (Exception ex)
            {

                return products;
            }

        }

        private HashSet<MerchantProductDetails> AddProducts(OleDbDataReader reader)
        {
            var _products = new HashSet<MerchantProductDetails>(new CompareProduct());
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _products.Add(new MerchantProductDetails
                        {
                            Barcode = reader["Barcode"].ToString(),
                            SubCategory = (reader["SubCategory"].ToString() != "") ? reader["SubCategory"].ToString() : "",
                            Category = (reader["Category"].ToString() != "") ? reader["Category"].ToString() : "",
                            //Volume = reader["Volume"].ToString(),
                            //Unit = reader["Unit"].ToString(),
                            NameAR = (reader["NameAR"].ToString() != "") ? reader["NameAR"].ToString() : "",
                            NameEN = (reader["NameEN"].ToString() != "") ? reader["NameEN"].ToString() : "",
                        });
                    }
                }
                return _products;
            }
            catch (Exception ex)
            {
                return _products;
            }


        }
        public MerchantComparedProducts CompareFiles(string path, string action)
        {
            var returnedData = new MerchantComparedProducts();

            if (action == "match") returnedData.MatchedProduct = GetMatchedItems(path);
            else returnedData.UnmatchedProduct = GetUnMatchedItems(path);

            return returnedData;
        }
        public HashSet<ProductDetails> GetMatchedItems(string path)
        {
            var products = ReadFromExcel(path);

            var _matchedProducts = new HashSet<ProductDetails>();
            using (var _db = new ApplicationDbContext())
            {
                var subCat = "";
                var itemsList = _db.Items.ToList();
                var subCatList = _db.SubCategories.ToList();
                foreach (var item in products)
                {
                    var results = itemsList.FirstOrDefault(e => e.Barcode == item.Barcode);
                    if (results != null)
                    {
                        subCat = subCatList.FirstOrDefault(e => e.Id == results.SubCategoryId).NameEN;
                        var _item = new ProductDetails
                        {
                            Barcode = item.Barcode.ToString(),
                            SubCategory = subCat,
                            Price = float.Parse(item.Price.ToString()),
                            ItemId = results.Id,
                            NameAR = results.NameAR,
                            NameEN = results.NameEN,
                            Volume = results.Volume,
                            SubCategoryId = results.SubCategoryId,
                            ImageUrl = results.ImageUrl
                        };
                        subCat = "";
                        _matchedProducts.Add(_item);
                    }
                }
            }
            // RemoveFileFromServer(path);
            return _matchedProducts;
        }
        public HashSet<ProductDetails> GetUnMatchedItems(string path)
        {
            var products = ReadFromExcel(path);
            var _unMatchedProducts = new HashSet<ProductDetails>();
            using (var _db = new ApplicationDbContext())
            {
                var itemsList = _db.Items.ToList();
                var subCatList = _db.SubCategories.ToList();
                foreach (var item in products)
                {
                    var results = itemsList.FirstOrDefault(e => e.Barcode == item.Barcode);
                    if (results == null)
                    {
                        var _item = new ProductDetails
                        {
                            Barcode = item.Barcode.ToString(),
                            Price = float.Parse(item.Price.ToString()),
                            //ItemId = item.Id,
                            NameAR = item.NameAR,
                            // NameEN = item.NameEN,
                            // Volume = item.Volume,
                        };
                        _unMatchedProducts.Add(_item);
                    }

                }
            }
            //RemoveFileFromServer(path);
            //CreateExcel(@"D:\Tasks\Excel\unmatched.xlsx", "Items", _unMatchedProducts);
            return _unMatchedProducts;
        }



        public HashSet<ProductDetails> GetMatchedItemsByNames(string path)
        {
            var products = ReadFromExcel(path);

            var _matchedProducts = new HashSet<ProductDetails>();
            using (var _db = new ApplicationDbContext())
            {
                var subCat = "";
                var itemsList = _db.Items.ToList();
                var subCatList = _db.SubCategories.ToList();
                foreach (var item in products)
                {
                    var results = itemsList.FirstOrDefault(e => e.NameAR.Contains(item.NameAR) || e.Barcode == item.Barcode);
                    if (results != null)
                    {
                        subCat = subCatList.FirstOrDefault(e => e.Id == results.SubCategoryId).NameEN;
                        var _item = new ProductDetails
                        {
                            Barcode = item.Barcode.ToString(),
                            SubCategory = subCat,
                            Price = float.Parse(item.Price.ToString()),
                            ItemId = results.Id,
                            NameAR = results.NameAR,
                            NameEN = results.NameEN,
                            Volume = results.Volume,
                            SubCategoryId = results.SubCategoryId,
                            ImageUrl = results.ImageUrl
                        };
                        subCat = "";
                        _matchedProducts.Add(_item);
                    }
                }
            }
            // RemoveFileFromServer(path);
            //CreateExcel(@"D:\Tasks\Excel\matched.xlsx", "Items", _matchedProducts);
            return _matchedProducts;
        }
        public HashSet<ProductDetails> GetUnMatchedItemsByNames(string path)
        {
            var products = ReadFromExcel(path);
            var _unMatchedProducts = new HashSet<ProductDetails>();
            using (var _db = new ApplicationDbContext())
            {
                var itemsList = _db.Items.ToList();
                var subCatList = _db.SubCategories.ToList();
                foreach (var item in products)
                {
                    var results = itemsList.FirstOrDefault(e => e.NameAR == item.NameAR);
                    if (results == null)
                    {
                        var _item = new ProductDetails
                        {
                            Barcode = item.Barcode.ToString(),
                            Price = float.Parse(item.Price.ToString()),
                            //ItemId = item.Id,
                            NameAR = item.NameAR,
                            //NameEN = item.NameEN,
                            // Volume = item.Volume,
                        };
                        _unMatchedProducts.Add(_item);
                    }

                }
            }
            //RemoveFileFromServer(path);
            //CreateExcel(@"D:\Tasks\Excel\unmatched.xlsx", "Items", _unMatchedProducts);
            return _unMatchedProducts;
        }

        public MerchantComparedProducts CompareFilesByItemsNames(string path, string action)
        {
            var returnedData = new MerchantComparedProducts();

            if (action == "match") returnedData.MatchedProduct = GetMatchedItemsByNames(path);
            else returnedData.UnmatchedProduct = GetUnMatchedItemsByNames(path);

            return returnedData;
        }

        public HashSet<ProductDetails> CompareMatchedProduct(string path, IEnumerable<ProductDetails> data)
        {
            var _matchedProducts = new HashSet<ProductDetails>();

            var products = ReadFromExcel(path);

            using (var _db = new ApplicationDbContext())
            {
                foreach (var item in products)
                {
                    var results = _db.Items.FirstOrDefault(e => e.Barcode == item.Barcode);

                    if (results != null)
                    {
                        _matchedProducts.Add(new ProductDetails
                        {
                            Id = item.Id,
                            Barcode = item.Barcode,
                            SubCategoryId = item.SubCategoryId,
                            Price = int.Parse(item.Price),
                            Quantity = int.Parse(item.Quantity),
                            ItemId = results.Id,
                            NameAR = results.NameAR,
                            NameEN = results.NameEN,
                            ImageUrl = results.ImageUrl
                        });
                    }
                }
            }

            return _matchedProducts;
        }
        public HashSet<ProductDetails> CompareUnMatchedProduct(string path, IEnumerable<ProductDetails> data)
        {
            var _unmatchedProducts = new HashSet<ProductDetails>();

            var products = ReadFromExcel(path);

            using (var _db = new ApplicationDbContext())
            {
                foreach (var item in products)
                {
                    var results = _db.Items.FirstOrDefault(e => e.Barcode == item.Barcode);

                    if (results == null)
                    {
                        _unmatchedProducts.Add(new ProductDetails
                        {
                            Id = item.Id,
                            Barcode = item.Barcode,
                            //SubCategoryId = item.SubCategoryId,
                            Price = int.Parse(item.Price),
                            //Quantity = int.Parse(item.Quantity),
                            //ItemId = results.Id,
                            NameAR = item.NameAR,
                            //NameEN = results.NameEN,

                        });
                    }
                }
            }

            return _unmatchedProducts;
        }
        public void InsertMerchantItems(List<ProductDetails> items, int _merchantId)
        {
            using (var _db = new ApplicationDbContext())
            {
                foreach (var item in items)
                {
                    _db.MerchantProducts.Add(new MerchantProduct
                    {
                        ItemId = item.ItemId,
                        MerchantId = _merchantId,
                        Price = int.Parse(item.Price.ToString()),
                        Quantity = item.Quantity,
                        SubCategoryId = item.SubCategoryId
                    });
                }
                _db.SaveChanges();
            }
        }

        public void SeperateItemsFromExcel(HttpPostedFileBase file, string mapPath)
        {
            var items = ReadFromExcel(GetPath(file, mapPath));
            var packItems = new List<MerchantProductDetails>();
            var collectionItems = new List<MerchantProductDetails>();
            try
            {
                foreach (var item in items)
                {
                    if (item.NameAR != "" && item.NameAR.Contains("*"))
                    {
                        var str = item.NameAR.Split('*');
                        var vQ = Regex.Match(str[0], @"((?:\d*\.)?(?:\d*\/*)\d+)").Value;
                        if (vQ != "")
                        {
                            var listNames = str[0].Split(vQ.ToCharArray());
                            item.Quantity = Regex.Match(str[1], @"((?:\d*\.)?(?:\d*\/*)\d+)").Value;
                            item.Volume = vQ + " " + listNames[vQ.Length];
                            item.NameAR = listNames[0];
                        }
                        collectionItems.Add(item);
                    }
                    else
                    {
                        var vQ = Regex.Match(item.NameAR, @"((?:\d*\.)?(?:\d*\/*)\d+)").Value;
                        if (vQ != "")
                        {
                            var listNames = item.NameAR.Split(vQ.ToCharArray());
                            item.Volume = vQ + " " + listNames[vQ.Length];
                            item.NameAR = listNames[0];
                        }
                        item.Volume = "1 gr";
                        item.Quantity = "1";
                        packItems.Add(item);
                    }
                }


                CreateExcel<MerchantProductDetails>(@"D:\Tasks\Excel\collectionItmes.xlsx", "Items", collectionItems);
                CreateExcel<MerchantProductDetails>(@"D:\Tasks\Excel\packItems.xlsx", "Items", packItems);

            }
            catch (System.Exception ex)
            {
            }
        }

        public string GetPath(HttpPostedFileBase file, string mapPath)
        {
            var path = "";
            if (file != null && file.ContentLength > 0)
            {
                path = mapPath + file.FileName;

                using (var fileStream = System.IO.File.Create(path))
                {
                    file.InputStream.Seek(0, SeekOrigin.Begin);
                    file.InputStream.CopyTo(fileStream);
                }
            }
            return path;
        }


        public void RemoveFileFromServer(string path)
        {
            try
            {
                if (path.Contains("Excel_Files"))
                    File.Delete(path);
            }
            catch (Exception ex)
            {

            }

        }

        public bool UpdateImagePath(List<string> list, string filePath)
        {
            try
            {
                var results = db.Items.Where(e => e.ImageUrl != null).ToList();//.Select(e => new ItemImage { ImageUrl = e.ImageUrl, Id = e.Id }).ToList();
                foreach (var item in list)
                {
                    var path = item.Split('.')[0];
                    var obj = results.FirstOrDefault(e => e.ImageUrl.Substring(e.ImageUrl.LastIndexOf('/') + 1).Split('.')[0] == path);
                    if (obj != null)
                    {
                        WriteToFile(filePath, new List<string> { string.Format("{0} -- {1} -- {2}", obj.Barcode, obj.ImageUrl, "https://s3.amazonaws.com/weelo/product/"+item)});
                        obj.ImageUrl = "https://s3.amazonaws.com/weelo/product/" + item;
                        if (obj.NameEN == null) obj.NameEN = "Null";
                        db.SaveChanges();
                    }
                    else
                        WriteToFile(filePath, new List<string> { string.Format("{0} Does not exist", item) });

                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        WriteToFile(filePath, new List<string> { string.Format("Property: {0} Error: {1}" , validationError.PropertyName,validationError.ErrorMessage)});
                    }
                }
                
                return false;
            }
        }


        public bool UpdateItems(string filePath)
        {
            try
            {
                var items = db.Items.ToList();
                var cats = db.Categories.ToList();
                var subCats = db.SubCategories.ToList();

                var result = ReadFromExcel(@"D:\Tasks\filterdList.xlsx");
                foreach (var item in result)
                {
                    var obj = items.FirstOrDefault(e => e.Barcode == item.Barcode);
                    var catObj = cats.FirstOrDefault(e => e.NameEN == item.Category || e.NameAR == item.Category);
                    var SubObj = subCats.FirstOrDefault(e => e.NameEN == item.SubCategory || e.NameAR == item.SubCategory);
                    if (obj != null)
                    {
                        WriteToFile(filePath, new List<string> { string.Format("{0}- {1} : {2}-{3} under Category {4} and SubCategory {5} ", obj.NameAR, obj.NameEN, item.NameAR, item.NameEN,catObj.NameEN,SubObj.NameEN) });
                        obj.NameAR = item.NameAR;
                        obj.NameEN = item.NameEN;
                        obj.SubCategoryId = SubObj.Id;
                        SubObj.CategoryId = catObj.Id;
                        db.SaveChanges();
                    }
                    else
                    {
                        WriteToFile(filePath, new List<string> { string.Format("{0} Does not exist", item) });
                    }
                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        WriteToFile(filePath, new List<string> { string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) });
                    }
                }

                return false;
            }
        }

        public void Convert2Json()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@"D:\Tasks\fo.json"))
                {
                    var csv = new List<MerchantProductDetails>();
                    var lines = System.IO.File.ReadAllLines(@"D:\Tasks\fouda.csv");
                    foreach (string line in lines)
                        csv.Add(new MerchantProductDetails
                        {
                            Barcode = line.Split(',')[0],
                            NameAR = line.Split(',')[1],
                            Price = line.Split(',')[2]
                        });
                    var serialize = new System.Web.Script.Serialization.JavaScriptSerializer();
                    serialize.MaxJsonLength = 6297152;
                    string json = serialize.Serialize(csv);
                    sw.Write(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public void WriteToFile(string path, List<string> lines)
        {
            using (StreamWriter sw = System.IO.File.AppendText(path))
            {
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }

            }
        }
    }

    public class MerchantComparedProducts
    {
        public HashSet<ProductDetails> MatchedProduct { get; set; }
        public HashSet<ProductDetails> UnmatchedProduct { get; set; }
        public int MerchantId { get; set; }
        public string Path { get; set; }
    }
    public class ItemImage
    {
        public string ImageUrl { get; set; }
        public int Id { get; set; }
    }
}


