using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testMVC.Dtos
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
