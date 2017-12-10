using AMS.Models;
using AMS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Helper
{
    public class CompareProduct : IEqualityComparer<MerchantProductDetails>
    {
        public bool Equals(MerchantProductDetails x, MerchantProductDetails y)
        {
            if (x == null && x == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (x.Barcode == null || y.Barcode == null)
                return false;
            else if (x.Barcode == y.Barcode)
                return true;
            else
                return false;
        }
        public int GetHashCode(MerchantProductDetails obj)
        {
            return obj.Barcode.GetHashCode();
        }


    }
}