using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testMVC.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public int Room { get; set; }
        public string Name { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
    }
}
