using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class Courier
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Courier name is required")]
        [Display(Name = "Courier name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mobile is required")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Merchant required")]
        [Display(Name = "Merchant")]
        public int MerchantId { get; set; }

    }
}
