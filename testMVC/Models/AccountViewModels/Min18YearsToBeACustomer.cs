using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace testMVC.Models.AccountViewModels
{
    public class Min18YearsToBeACustomer : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var user = (AccountViewModels.RegisterViewModel)validationContext.ObjectInstance;
            var user = (RegisterViewModel)validationContext.ObjectInstance;

            //Console.WriteLine("18year");

            if (user.BirthDate == null)
                return new ValidationResult("Birthdate is required.");

            var age = DateTime.Today.Year - user.BirthDate.Value.Year;

            return (age >=18) 
                ? ValidationResult.Success 
                : new ValidationResult("You should be at least 18 years old to use our service.");
        }
    }
}
