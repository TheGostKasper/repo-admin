using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Add Currency name")]
        [Display(Name = "Currency name")]
        public string Name { get; set; }
    }
}
