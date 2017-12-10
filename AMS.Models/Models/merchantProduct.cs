using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Models
{
    public class MerchantProduct
    {
        [Key]
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int ItemId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public int? SubCategoryId { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
