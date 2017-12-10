using System;

namespace AMS.Models.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public int SubCategoryId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public int ItemId { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string SubCategory { get; set; }
        public string Volume { get; set; }
        public string ImageUrl { get; set; }

        public string getHeader()
        {
            return "Barcode,Name,Volume";
        }
    }
    public class ExcelReorder
    {
        //public string Id { get; set; }
        public string NameAR { get; set; }
        //public string BuyPrice { get; set; }
        //public string Sellprice { get; set; }
        public string Quantity { get; set; }
        public string Volume { get; internal set; }
        public string Unit { get; set; }
        public string Barcode { get; set; }
        //public string NameEN { get; internal set; }
        //public string Price { get; internal set; }

        public override string ToString()
        {
            return "Name,Quntity,Volume,Unit,Barcode";
        }
    }

}
