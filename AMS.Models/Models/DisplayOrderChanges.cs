using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
   public class DisplayOrderChanges
    {
        public int Id { get; set; }
        public List<DisplayOrder> Ctgs { get; set; }
    }

    public class DisplayOrder
    {
        public int Id { get; set; }
        public int Index { get; set; }
    }
}
