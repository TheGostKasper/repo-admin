using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class ItemDetails
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string Volume { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
