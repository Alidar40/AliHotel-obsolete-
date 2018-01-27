using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using testMVC.Models;
using testMVC.Dtos;
using AutoMapper;
using Newtonsoft.Json;


namespace testMVC.Controllers.Api
{
    [Route("api/[controller]")]
    public class RoomsController : Controller 
    {
        private ApplicationDbContext _context;
        public RoomsController(ApplicationDbContext context)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnet-identity-BB984385-CB7D-4F7D-829B-DEAF83487460;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            _context = new ApplicationDbContext(options);
            this._context = context;
        }
        
        // GET /api/rooms
        [HttpGet]
        public IActionResult GetRooms()
        {
            foreach (var c in _context.Rooms)
            {
                foreach (var r in _context.RoomTypes)
                {
                    if (c.RoomTypeId == r.Id)
                    {
                        c.RoomType = r;
                    }
                }
            }

            var roomDtos = _context.Rooms
                 .Include(c => c.RoomType)
                 .ToList()
                 .Select(Mapper.Map<Room, RoomDto>);

            if (User.IsInRole("admin"))
            {
                roomDtos = _context.Rooms.ToList().Select(Mapper.Map<Room, RoomDto>);
                return Ok(roomDtos);
            };

            roomDtos = _context.Rooms.ToList().Where(r => r.IsOccupied == false).Select(Mapper.Map<Room, RoomDto>);

            return Ok(roomDtos);
        }

        // GET /api/rooms/1
        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _context.Rooms.Include(c => c.RoomType).SingleOrDefault(c => c.Id == id);

            if (room == null)
                return null;
            

            return Ok(Mapper.Map<Room, RoomDto>(room));
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