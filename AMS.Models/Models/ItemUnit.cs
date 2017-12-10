using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class ItemUnit
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Add Unit Name")]
        [Display(Name = "Unit name")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Add Unit Shortcut")]
        [Display(Name = "Unit Shortcut")]
        public string Name { get; set; }

    }
}
