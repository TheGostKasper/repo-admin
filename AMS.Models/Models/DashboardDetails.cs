using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Models
{
    public class DashboardDetails
    {
        public int AcceptedOrders { get; set; }
        public int CanceledOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public int Merchants { get; set; }
        public int Orders { get; set; }
        public int PendingOrders { get; set; }
        public int ReadyOrders { get; set; }
        public int Users { get; set; }
    }
}
