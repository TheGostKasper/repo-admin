using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Models
{
    public class MerchantProductPaging
    {
        public List<MerchantProductDetails> Data { get; set; }
        public HashSet<ProductDetails> MatchedProduct { get; set; }
        public HashSet<ProductDetails> ProductDetails { get; set; }
        public HashSet<ProductDetails> UnmatchedProduct { get; set; }
        public int MatchedProductTotal { get; set; }
        public int UnmatchedProductTotal { get; set; }
        public int Total { get; set; }

    }

    public class MItemPaging<T>
    {
        public int Total { get; set; }
        public List<T> Data { get; set; }

    }
}
