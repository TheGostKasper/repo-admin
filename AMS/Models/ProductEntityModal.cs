using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models
{
    public class ProductEntityModal
    {
        public int Id { get; set; }

       
        public string NameEN { get; set; }

        public string NameAR { get; set; }

        public int SubCategoryId { get; set; }

        public double Volume { get; set; }

        public int UnitId { get; set; }

        public string ImageUrl { get; set; }

        public string Barcode { get; set; }

        public override string ToString()
        {
            return "ID,Barcode,NameEN,NameAR,SubCategoryId,Volume,UnitId,ImageUrl";
        }
    }
}