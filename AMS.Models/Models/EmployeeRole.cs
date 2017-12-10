using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class EmployeeRole
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Add employee")]
        [Display(Name = "Employee Name")]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Add role")]
        [Display(Name = "Employee Role")]
        public int RoleId { get; set; }
    }
}
