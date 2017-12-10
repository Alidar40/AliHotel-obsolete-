using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace testMVC.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }

        [Required]
        public RoomType RoomType { get; set; }
        public byte RoomTypeId { get; set; }
        //Type: 1 - standart, 2 - junior suite 3 - suite
        //public int Type { get; set; }

        public bool IsOccupied { get; set; }
        public int Floor { get; set; }
        public float Price { get; set; }
    }
}
