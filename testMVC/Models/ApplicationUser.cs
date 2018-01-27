using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using testMVC.Models.AccountViewModels;

namespace testMVC.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool IsRenter { get; set; }

        //public int RentalId { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [StringLength(12)]
        public string Phone { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsToBeACustomer]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "Сredit card number")]
        [StringLength(20)]
        public string CreditCard { get; set; }
    }
    public class ApplicationRole : IdentityRole { }
}
