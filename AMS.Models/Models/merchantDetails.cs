using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Models
{
    public class MerchantDetails
    {
        public string AccountState { get; set; }
        public int AccountStateId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Currency { get; set; }
        public int CurrencyId { get; set; }
        public int Id { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string Phone { get; set; }
    }
}
