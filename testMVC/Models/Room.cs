using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testMVC.Models
{
    public class Room
    {
        public int Number { get; set; }
        public int Capacity { get; set; }
        //Type: 1 - standart, 2 - junior suite 3 - suite
        public int Type { get; set; }
        public bool IsOccupied { get; set; }
    }
}
