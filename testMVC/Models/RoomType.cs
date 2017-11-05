using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace testMVC.Models
{
    public class RoomType
    {
        public int Id { get; set; }

         [Required]
         [StringLength(255)]
         public string Name { get; set; }
    }
}
