using AMS.Context;
using AMS.Helper;
using AMS.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    [Authorize]
    public class UploadController : SecurityController
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        public async Task<JsonResult> Document()
        {
            try
            {
                 string[] mimeList = { "jpg", "png" };
            var fileList = new List<Document>();
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var gName = Guid.NewGuid().ToString();

                        //fileName = Regex.Replace(fileName, @"\s+", "");
                        
                        var fileExt = Path.GetExtension(fileName).Substring(1, Path.GetExtension(fileName).Length - 1);
                        var fileUrl = string.Format("{0}{1}.{2}", "/Content/Upload/", gName, fileExt);
                        var filePath = string.Format("{0}{1}.{2}", Server.MapPath("~/Content/Upload/"), gName, fileExt);
                        
                        if (mimeList.Contains(fileExt))
                        {
                            file.SaveAs(filePath);
                            fileList.Add(new Document()
                            {
                                Name = fileName,
                                Size = FileSizeFormat(file.ContentLength),
                                Url = fileUrl,
                                Type = fileExt,
                                Status = (int)HttpStatusCode.OK,
                                Message = "Uploaded successfully!"
                            });
                            // Save file in DB 
                            //db.Application_Document.Add(new Application_Document()
                            //{
                            //    Application_Id = int.Parse(Request.Form["ApplicationId"].ToString()),
                            //    Name = fileName,
                            //    Url = fileUrl,
                            //    Type = fileExt,
                            //    Size = FileSizeFormat(file.ContentLength)

                            //});
                           // await db.SaveChangesAsync();
                        }
                        else
                        {
                            fileList.Add(new Document()
                            {
                                Name = fileName,
                                Size = FileSizeFormat(file.ContentLength),
                                Url = fileUrl,
                                Type = fileExt,
                                Status = (int)HttpStatusCode.UnsupportedMediaType,
                                Message = "This file extension is not supported"
                            });
                        }


                    }
                }
                return Json(new { data = fileList, message = HttpStatusCode.OK });
            }
            return Json(new { data = fileList, message = HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { });
                Log.Error(ex.Message, ex);
                throw;
            }

        }
       
        [HttpPost]
        public async Task<JsonResult> Logo()
        {
            try
            {
                string[] mimeList = { "jpg", "png" };
                var fileList = new List<Document>();
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var gName = Guid.NewGuid().ToString();

                            //fileName = Regex.Replace(fileName, @"\s+", "");

                            var fileExt = Path.GetExtension(fileName).Substring(1, Path.GetExtension(fileName).Length - 1);
                            var fileUrl = string.Format("{0}{1}.{2}", "/Content/Upload/logo/", gName, fileExt);
                            var filePath = string.Format("{0}{1}.{2}", Server.MapPath("~/Content/Upload/logo/"), gName, fileExt);

                            if (mimeList.Contains(fileExt))
                            {
                                file.SaveAs(filePath);
                                fileList.Add(new Document()
                                {
                                    Name = fileName,
                                    Size = FileSizeFormat(file.ContentLength),
                                    Url = fileUrl,
                                    Type = fileExt,
                                    Status = (int)HttpStatusCode.OK,
                                    Message = "Uploaded successfully!"
                                });
                            }
                            else
                            {
                                fileList.Add(new Document()
                                {
                                    Name = fileName,
                                    Size = FileSizeFormat(file.ContentLength),
                                    Url = fileUrl,
                                    Type = fileExt,
                                    Status = (int)HttpStatusCode.UnsupportedMediaType,
                                    Message = "This file extension is not supported"
                                });
                            }


                        }
                    }
                    return Json(new { data = fileList, message = HttpStatusCode.OK });
                }
                return Json(new { data = fileList, message = HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { });
                Log.Error(ex.Message, ex);
                throw;
            }

        }
        private string FileSizeFormat(double len)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (len >= 1024 && ++order < sizes.Length)
            {
                len = len / 1024;
            }
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }

    }
}

public class Document
{
    public string Name { get; set; }
    public string Size { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public int Status { get; set; }
    public string Message { get; set; }
}
