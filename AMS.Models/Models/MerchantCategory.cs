using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class MerchantCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Building name is required")]
        [Display(Name = "Building name")]
        public int BuildingId { get; set; }

        [Required(ErrorMessage = "Mechant name is required")]
        [Display(Name = "Merchant name")]
        public int MerchantId { get; set; }
    }
}
