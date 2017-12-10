using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models
{
    public class MPDetail
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}