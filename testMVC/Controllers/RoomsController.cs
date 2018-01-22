using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using testMVC.Data;

namespace testMVC.Controllers
{
    public class RoomsController : Controller
    {
        private ApplicationDbContext _context;

        public RoomsController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AliHotelDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            _context = new ApplicationDbContext(options);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            //var room = GetRoom();

            var room = _context.Rooms.ToList();

            return View(room);
        }

        public IActionResult Details(int id)
        {
            //var room = GetCustomers().SingleOrDefault(c => c.Id == id);

            var room = _context.Rooms.SingleOrDefault(c => c.Id == id);

            if (room == null)
                return NotFound();

            return View(room);
        }

        /*
        private IEnumerable<Room> GetRoom()
        {
            return new List<Room>
            {
                new Room { Number = 1, Capacity = 4, IsOccupied = true, Type = 1},
                new Room { Number = 2, Capacity = 4, IsOccupied = true, Type = 2}
            };
        }
        */
    }
}