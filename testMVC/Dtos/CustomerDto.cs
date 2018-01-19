using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using testMVC.Models;

namespace testMVC.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        //[Required]
        public byte RoomId { get; set; }
        public RoomDto Room { get; set; }

        [Required(ErrorMessage = "Please enter customer's name")]
        [StringLength(255)]
        public string Name { get; set; }
        
        //[Min18YearsToBeACustomer]
        public DateTime? BirthDate { get; set; }
        
        public DateTime? ArrivalDate { get; set; }
        
        public DateTime? DepartureDate { get; set; }
    }
}
