using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Country name is required")]
        [Display(Name = "English name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Country name is required")]
        [Display(Name = "Arabic name")]
        public string NameAR { get; set; }

    }
}
