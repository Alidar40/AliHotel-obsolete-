using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testMVC.Models;

namespace testMVC.Controllers
{
    public class RoomsController : Controller
    {
        public ViewResult Index()
        {
            var room = GetRoom();

            return View(room);
        }

        private IEnumerable<Room> GetRoom()
        {
            return new List<Room>
            {
                new Room { Number = 1, Capacity = 4, IsOccupied = true, Type = 1},
                new Room { Number = 2, Capacity = 4, IsOccupied = true, Type = 2}
            };
        }
    }
}