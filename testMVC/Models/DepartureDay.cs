using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace testMVC.Models
{
    public class DepartureDay : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var user = (AccountViewModels.RegisterViewModel)validationContext.ObjectInstance;
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.DepartureDate == null)
                return new ValidationResult("Departure date must be entered.");

            if (customer.ArrivalDate == null)
                return new ValidationResult("You haven't yet entered arrival date");

            //var age = DateTime.Today.Day - customer.DepartureDate.Value.Day;
            var day = customer.ArrivalDate.Value.Day - customer.DepartureDate.Value.Day;
            var month = customer.ArrivalDate.Value.Month - customer.DepartureDate.Value.Month;
            var year = customer.ArrivalDate.Value.Year - customer.DepartureDate.Value.Year;

            return ((year <= 0) && (month <= 0 ) && (day < 0))
                ? ValidationResult.Success
                : new ValidationResult("Incorrect departure date.");
        }
    }
}
