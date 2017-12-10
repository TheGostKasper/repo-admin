using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Models
{
    public class MerchantRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Path { get; set; }
    }
}
