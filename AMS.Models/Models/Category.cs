using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Add category name (EN)")]
        [Display(Name = "Category name (EN)")]
        public string NameEN { get; set; }
        [Required(ErrorMessage = "Add category name (AR)")]
        [Display(Name = "Category name (AR)")]
        public string NameAR { get; set; }
        [Required(ErrorMessage = "Select country")]
        [Display(Name = "Country name")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Select category image")]
        [Display(Name = "Category image")]
        public string ImageUrl { get; set; }
        //[Required(ErrorMessage = "Select category image")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}
