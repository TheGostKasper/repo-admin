﻿
using System;

namespace AMS.Models
{
    public class MerchantCouponDetails
    {
        public int Id { get; set; }
        public string MerchantName { get; set; }
        public string CouponeTypeName { get; set; }
        public string CouponeText { get; set; }
        public int Amount { get; set; }
        public DateTime?  CreationDate{ get; set; }
        public DateTime?  ExpiryDate{ get; set; }
    }
}