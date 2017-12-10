using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Add SubCategory name (EN)")]
        [Display(Name = "SubCategory name (EN)")]
        public string NameEN { get; set; }
        [Required(ErrorMessage = "Add SubCategory name (AR)")]
        [Display(Name = "SubCategory name (AR)")]
        public string NameAR { get; set; }
        [Required(ErrorMessage = "Select Category")]
        [Display(Name = "Category name")]
        public int CategoryId { get; set; }


    }
}
