using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UUID { get; set; }
        public string DeviceToken { get; set; }
        //public AppPlatform Platform { get; set; }
        public int CountryId { get; set; }
        public string Mobile { get; set; }
        public string SmsCode { get; set; }
        public bool Verified { get; set; }
        public DateTime CreationDate { get; set; }
        //public Address Address { get; set; }
    }
}
