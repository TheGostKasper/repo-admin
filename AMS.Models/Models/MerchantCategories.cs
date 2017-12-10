using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
   public class MerchantCategories
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int MerchantId { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
