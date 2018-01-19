using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testMVC.Models;

namespace testMVC.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<Room> Rooms { get; set; }
        public Customer Customer { get; set; }
    }
}
