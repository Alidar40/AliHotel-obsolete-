using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testMVC.Models
{
    public class Customer
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        //public int Room { get; set; }
        
        //[Required]
        public Room Room { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = "Please select room")]//
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Please enter customer's name")]//
        [StringLength(255)]
        public string Name { get; set; }

        [Required]//
        public string UserId { get; set; }

        //[Required]
        public ApplicationUser User { get; set; }

        [Display(Name = "Date of Arrival")]
        [DataType(DataType.Date)]
        //[ArrivalDate]
        public DateTime? ArrivalDate { get; set; }

        [Display(Name = "Date of Departure")]
        [DataType(DataType.Date)]
        [DepartureDay]
        public DateTime? DepartureDate { get; set; }

        public bool IsClosed { get; set; }

        public float Bill { get; set; }
    }
}
