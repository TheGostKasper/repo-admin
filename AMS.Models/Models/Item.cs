using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Add Item name (EN)")]
        [Display(Name = "Item name (EN)")]
        public string NameEN { get; set; }

        [Required(ErrorMessage = "Add Item name (AR)")]
        [Display(Name = "Item name (AR)")]
        public string NameAR { get; set; }

        [Required(ErrorMessage = "Select SubCategory")]
        [Display(Name = "SubCategory name")]
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Add Item volume")]
        [Display(Name = "Item volume")]
        public string Volume { get; set; }

        [Required(ErrorMessage = "Select Item unit")]
        [Display(Name = "Item unit")]
        public int? UnitId { get; set; }

        //[Required(ErrorMessage = "Select Item image")]
        [Display(Name = "Item image")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Barcode is required")]
        [Display(Name = "Barcode")]
        public string Barcode { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}

