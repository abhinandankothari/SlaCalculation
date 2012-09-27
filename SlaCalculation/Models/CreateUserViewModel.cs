using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SlaCalculation.Models
{
    public class CreateUserViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(6, ErrorMessage = "The Employee ID must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Employee ID")]
        public string EmployeeId { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$",ErrorMessage = "Enter Email Address in Correct Format")]
        [Display(Name = "Email ID")]
        public string Email { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Mobile Number")]
        [StringLength(14, ErrorMessage = "The Employee ID must be at least {2} characters long.", MinimumLength = 10)]
        public string Mobile { get; set; }
    }
}