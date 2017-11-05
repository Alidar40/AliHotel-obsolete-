using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace testMVC.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public int Room { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
        //public DateTime? BirthDate { get; set; }

    }
}
