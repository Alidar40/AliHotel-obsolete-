using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace testMVC.Models
{
    public class ArrivalDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var user = (AccountViewModels.RegisterViewModel)validationContext.ObjectInstance;
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.ArrivalDate == null)
                return new ValidationResult("Arrival date must be entered.");

            var age = DateTime.Today.Day - customer.ArrivalDate.Value.Day;

            return (age <= 0)
                ? ValidationResult.Success
                : new ValidationResult("Incorrect arrival date.");
        }
    }
}
