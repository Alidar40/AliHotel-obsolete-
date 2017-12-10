using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace testMVC.Models
{
    public class Customer
    {
        public int Id { get; set; }
        //public int Room { get; set; }

        //[Required]
        public Room Room { get; set; }
        public byte RoomId { get; set; }

        [Required(ErrorMessage = "Please enter customer's name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsToBeACustomer]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Date of Arrival")]
        public DateTime? ArrivalDate { get; set; }

        [Display(Name = "Date of Departure")]
        public DateTime? DepartureDate { get; set; }


    }
}
