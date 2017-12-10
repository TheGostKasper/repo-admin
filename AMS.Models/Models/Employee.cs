using System;
using System.ComponentModel.DataAnnotations;

namespace AMS.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
      
        [Required(ErrorMessage = "Add name")]
        [Display(Name = "Employee name")]
        public string Name { get; set; }
      
        [Required(ErrorMessage = "Add mobile")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Add email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Add Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Add address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Select employee status")]
        [Display(Name = "Account Status")]
        public int Status { get; set; }

        public DateTime CreationDate { get; set; }
        public string RolesIds { get; set; }
    }
}
