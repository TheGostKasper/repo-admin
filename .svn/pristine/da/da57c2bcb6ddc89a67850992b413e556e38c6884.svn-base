using AMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AMS.Helper
{
    public class VXInsertion
    {
        public HashSet<MPDetail> GetListOfTextFile(string filePath)
        {
            try
            {
                var list = new HashSet<MPDetail>();
                var file = File.ReadAllLines(filePath);
                for (int i = 0; i < file.Length; i++)
                {
                    var currObj = file[i].Split('_');
                    if (currObj.Length > 0)
                    {
                        list.Add(new MPDetail
                        {
                            Barcode = currObj[0],
                            NameAR = currObj[1],
                            NameEN = currObj[2],
                            Price = Convert.ToDouble(currObj[3]),
                            Quantity = Convert.ToInt32(currObj[4])
                        });
                        WriteToFile(new List<string> { string.Format("Item [{0}] & Barcode {1} added Successfully ", i, currObj[0]) });
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                WriteToFile( new List<string> { e.Message ,"Getting Product Exception"});
               throw;
            }
          
        }

        public string SaveFileToServerPath(HttpPostedFileBase file,string serverPath)
        {
            var path = "";
            if (file != null && file.ContentLength > 0)
            {
                path = serverPath + file.FileName;

                using (var fileStream = System.IO.File.Create(path))
                {
                    file.InputStream.Seek(0, SeekOrigin.Begin);
                    file.InputStream.CopyTo(fileStream);
                }
            }
            return path;
        }

        public void WriteToFile(List<string> lines)
        {
            using (StreamWriter sw = File.AppendText(@"D:\CODE\WEB\Weelo-Admin\AMS\Log\m-product.txt"))
            {
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }

            }
        }
    }
}