using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using testMVC.Models;

namespace testMVC.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name must be entered")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [StringLength(12, ErrorMessage = "Phone number must be entered")]
        public string Phone { get; set; }

        [Min18YearsToBeACustomer]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        //[Required]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "Сredit card number")]
        [StringLength(20, ErrorMessage = "Credit card number must be entered")]
        public string CreditCard { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
