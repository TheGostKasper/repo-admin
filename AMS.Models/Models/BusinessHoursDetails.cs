using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class BusinessHoursDetails
    {
        [Key]
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int MerchantName { get; set; }
        public int DayId { get; set; }
        public string DayName { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }


    public class BHDetails
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string Day { get; set; }
        public string DayName { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}
