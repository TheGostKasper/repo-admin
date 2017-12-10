using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class ItemType: BasicInfo
    {
        [Key]
        public int Id { get; set; }
    }
}
