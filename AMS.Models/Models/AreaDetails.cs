using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class AreaDetails
    {
        public int Id { get; set; }
        [Display(Name = "Area name")]
        public string Name { get; set; }
        public int CityId { get; set; }
        [Display(Name = "City name")]
        public string CityName { get; set; }
        [Display(Name = "Country name")]
        public string CountryName { get; set; }
        public string NameAR { get; set; }
    }
}
