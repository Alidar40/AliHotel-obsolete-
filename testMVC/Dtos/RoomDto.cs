using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using testMVC.Models;


namespace testMVC.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public int Number { get; set; }

        //[Required]
        public RoomTypeDto RoomType { get; set; }
        public byte RoomTypeId { get; set; }

        public int Capacity { get; set; }

        public float Price { get; set; }

        //Type: 1 - standart, 2 - junior suite 3 - suite
        //public int Type { get; set; }

        public bool IsOccupied { get; set; }
    }
}
