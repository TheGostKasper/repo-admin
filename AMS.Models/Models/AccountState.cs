using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class AccountState
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Add Account State")]
        [Display(Name = "Account State")]
        public string State { get; set; }
    }
}
