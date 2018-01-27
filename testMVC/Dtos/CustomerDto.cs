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
        public string Id { get; set; }

        //[Required]
        public int RoomId { get; set; }
        public RoomDto Room { get; set; }

        [Required(ErrorMessage = "Please enter customer's name")]
        [StringLength(255)]
        public string Name { get; set; }
        
        public string UserId { get; set; }
        
        public ApplicationUser User { get; set; }

        public DateTime? ArrivalDate { get; set; }
        
        public DateTime? DepartureDate { get; set; }
    }
}
