using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class BusinessHours
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Merchant is required")]
        [Display(Name = "Merchant Name")]
        public int MerchantId { get; set; }

        [Required(ErrorMessage = "Day is required")]
        [Display(Name = "Day")]
        public int Day { get; set; }

        [Required(ErrorMessage = "Opening Time is required")]
        [Display(Name = "Opening Time")]
        public string OpenTime { get; set; }

        [Required(ErrorMessage = "Closing Time is required")]
        [Display(Name = "Closing Time")]
        public string CloseTime { get; set; }
    }
}
