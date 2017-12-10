using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class MerchantCoupon
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Coupone (Name)")]
        public string CouponId { get; set; }
        public int Amount { get; set; }
        [Display(Name ="Type")]
        public int TypeId { get; set; }
        [Display(Name ="Merchant")]
        public int MerchantId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
