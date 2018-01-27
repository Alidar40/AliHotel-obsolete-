using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace testMVC.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Room Room { get; set; }

        public DateTime ArivalDate { get; set; }

        public DateTime DepartureDate { get; set; }
    }
}
