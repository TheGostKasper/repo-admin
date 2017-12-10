using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }
    }
}