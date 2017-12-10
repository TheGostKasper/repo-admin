using AMS.Controllers;
using AMS.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace AMS.Helper
{
    public class MerchantOperation
    {
        public static string _filePath = "";
        public static string _token = "";
        public MerchantOperation()
        {
        }

        public bool InsertProduct(string filePath, int mrchId)
        {
            try
            {
                var objs = GetListItem(filePath);
                foreach (var item in objs)
                {
                    var result = GetProductByBarcode(item.Barcode).Data[0];
                    if (result != null)
                    {
                        var itObj = result;
                        var mProduct = AddMerchantProduct(new ProductDTO
                        {
                            ProductId = itObj.Id,
                            Price = Convert.ToDouble(item.Price),
                            CreationDate = DateTime.UtcNow,
                            MerchantId = mrchId
                        });
                    }
                    else
                    {
                        var prod=AddProduct(new Item
                        {
                            Barcode = item.Barcode,
                            NameAR = item.NameAR,
                            NameEN = TranslateByApiKey(item.NameAR),
                            CreationDate = DateTime.UtcNow,
                            SubCategoryId = 1,
                            UnitId = null,
                            Volume = "1"
                        });
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                WriteToFile(new List<string> { ex.Message });
            }
            return false;

        }

        public List<MItemDetails> GetMatchedItems(string filePath)
        {
            try
            {
                var objs = GetListItem(filePath);
                var matchedItems = new List<MItemDetails>();

                foreach (var item in objs)
                {
                    var result = GetProductByBarcode(item.Barcode);
                    if (result.Data != null)
                    {
                        matchedItems.Add(new MItemDetails
                        {
                            Barcode = result.Data[0].Barcode,
                            NameAR = result.Data[0].NameAR,
                            Price = item.Price,
                            Image = result.Data[0].ImageUrl
                        });
                        WriteToFile(new List<string> { string.Format("{0} matched DB ",item.Barcode) });
                    }
                }
                return matchedItems;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        WriteToFile(new List<string>{ string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) });
                    }
                }

                return null;
            }
           
        }

        public List<MItemDetails> GetUnMatchedItems(string filePath)
        {
            try
            {
                var objs = GetListItem(filePath);
                var matchedItems = new List<MItemDetails>();

                foreach (var item in objs)
                {
                    var result = GetProductByBarcode(item.Barcode);
                    if (result.Data == null)
                    {
                        matchedItems.Add(new MItemDetails
                        {
                            Barcode = item.Barcode,
                            NameAR = item.NameAR,
                            Price = item.Price
                        });
                    }
                }
                return matchedItems;
            }
            catch (Exception ex)
            {
                WriteToFile(new List<string> { ex.Message });
                return null;
            }
        }
        public List<MItemDetails> GetListItem(string filePath)
        {
            var objs = File.ReadAllLines(filePath);
            var itms = objs.Select(e => new MItemDetails
            {
                Barcode = e.Split('\t')[0],
                NameAR = e.Split('\t')[1],
                Price = e.Split('\t')[2]
            }).ToList();
            WriteToFile(new List<string> { "Data fetched succ" });
            return itms;
        }
        public ApiResponse<List<Item>> GetProductByBarcode(string barcode)
        {
            try
            {
                var endPoint = "product/barcode/" + barcode;
                var response = AjaxCall(endPoint, new { }, Method.GET);
                var res = JsonConvert.DeserializeObject(response.Content);
                var obj = JsonConvert.DeserializeObject<ApiResponse<List<Item>>>(response.Content);
                if (obj.Data != null) WriteToFile(new List<string> { string.Format("{0} => Found {1} ", obj.Data[0].Barcode, obj.Data[0].CreationDate) });
                else WriteToFile(new List<string> { string.Format("Item has barcode ={0} => not found , DateTime ({1})", barcode, DateTime.Now) });

                return obj;
            }
            catch (Exception ex)
            {
                WriteToFile(new List<string> { ex.Message });
                return null;
            }
        }

        public ApiResponse<Item> AddMerchantProduct(ProductDTO product)
        {
            try
            {
                var endPoint = "merchant/product";// + barcode;
                var response = AjaxCall(endPoint, product, Method.GET);
                var obj = JsonConvert.DeserializeObject<ApiResponse<Item>>(response.Content);
                if (obj.Data != null)
                    WriteToFile(new List<string> { string.Format("{0} => inserted at {1} ", obj.Data.Barcode, obj.Data.CreationDate) });
                else
                    WriteToFile(new List<string> { string.Format("Item has Id ={0} => not found , DateTime ({1})", product.ProductId, DateTime.Now) });
                return obj;
            }
            catch (Exception ex)
            {
                WriteToFile(new List<string> { ex.Message });
                return null;
            }
        }
        public ApiResponse<Item> AddProduct(Item item)
        {
            try
            {
                var response = AjaxCall("product", item, Method.POST);
                var obj = JsonConvert.DeserializeObject<ApiResponse<Item>>(response.Content);
                if (obj.Data != null)
                    WriteToFile(new List<string> { string.Format("{0} => inserted at {1} ", obj.Data.Barcode, obj.Data.CreationDate) });
                else
                    WriteToFile(new List<string> { string.Format("Item has barcode ={0} => did not inserted, DateTime ({1})", item.Barcode, DateTime.Now) });
                return obj;
            }
            catch (Exception ex)
            {
                WriteToFile(new List<string> { ex.Message });
                return null;
            }
        }

        public IRestResponse AjaxCall(string endPoint, Object parameters, Method method)
        {
            try
            {
                var client = new RestClient(GetServerUrl() + "api/v1/" + endPoint);
                var request = new RestRequest(method);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", "Bearer " + _token);
                request.AddParameter("application/json", JsonConvert.SerializeObject(parameters), ParameterType.RequestBody);
                return client.Execute(request);
            }
            catch (Exception ex)
            {
                WriteToFile(new List<string> { ex.Message });
                return null;
            }
        }
        public string TranslateByApiKey(string text)
        {
            try
            {
                var result = "";
                string url = string.Format("https://translation.googleapis.com/language/translate/v2?key={0}&source={1}&target={2}&q={3}",
                    "AIzaSyAUhsDhNPjRlOEdezOcVyze8Lt1qjhumvk", "ar", "en", text);
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    JsonData jsonData = (new JavaScriptSerializer()).Deserialize<JsonData>(json);
                    result = jsonData.Data.Translations[0].TranslatedText;
                }
                return result;
            }
            catch (Exception ex)
            {
                    WriteToFile(new List<string> { "********* Start Exception **********", ex.Message, "********* End Exception ************" });
                return "";
            }
        }
        public void SaveToken(string token, string filePath)
        {
            _filePath = filePath;
            _token = token;
        }

        public static string GetServerUrl()
        {
            if (ConfigurationManager.AppSettings["LiveMode"] == "true")
                return ConfigurationManager.AppSettings["LiveServerUrl"].ToString();
            else
                return ConfigurationManager.AppSettings["TestServerUrl"].ToString();

        }

        public void WriteToFile(List<string> lines)
        {
            using (StreamWriter sw = System.IO.File.AppendText(_filePath))
            {
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }

            }
        }
    }
}