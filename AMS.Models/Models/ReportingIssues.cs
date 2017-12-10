using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Models
{
    public class ReportingIssues
    {
        [Key]
        public int Id { get; set; }
        public int merchantId { get; set; }
        public int OrderId { get; set; }
        public string Text { get; set; }
        public int StatusId { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
