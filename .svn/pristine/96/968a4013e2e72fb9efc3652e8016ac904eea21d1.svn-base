using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class MerchantSupervisor
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Supervisor name is required")]
        [Display(Name = "Supervisor name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mobile is required")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Merchant required")]
        [Display(Name = "Merchant")]
        public int MerchantId { get; set; }

    }
}
