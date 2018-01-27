using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using testMVC.Models;
using testMVC.Dtos;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace testMVC.Controllers.Api
{
    [Route("api/[controller]")]
    public class RentalsController: Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RentalsController(UserManager<ApplicationUser> userManager)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnet-identity-BB984385-CB7D-4F7D-829B-DEAF83487460;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            _context = new ApplicationDbContext(options);
            //this._context = context;

            _userManager = userManager;

        }

        // GET /api/customers
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetCustomers(byte switcher)
        {
            //var customerDtos = _context.Customers.FirstOrDefault();
            //_context.Entry(customerDtos).Reference(x => x.Room).Load();
            //_context.Rooms.Where(p => p.Id == customerDtos.).Load();

            //var customer = _context.Customers.FirstOrDefault();
            //_context.Rooms.Where(p => p.Id == customer.RoomId).Load();

            //var customerDtos = _context.Customers.Where(p => p.RoomId == Room.Id).Load();

            foreach (var c in _context.Customers)
            {
                foreach (var r in _context.Rooms)
                {
                    if (c.RoomId == r.Id)
                    {
                        c.Room = r;
                    }
                }
            }

            var customerDtos = _context.Customers
                    .Include(c => c.Room)
                    .Where(c => c.IsClosed == false)
                    .ToList();

            return Ok(customerDtos);
        }

        //DELETE/api/customers/1
        //[HttpDelete]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customerInDb = _context.Customers.Single(c => c.Id == id);
            var roomInDb = _context.Rooms.Single(r => r.Id == customerInDb.RoomId);
            ApplicationUser user = _userManager.FindByIdAsync(customerInDb.UserId).Result;

            customerInDb.Room = _context.Rooms.Single(r => r.Id == customerInDb.RoomId);
            //customerInDb.Room.IsOccupied = false;
            roomInDb.IsOccupied = false;

            customerInDb.IsClosed = true;
            customerInDb.DepartureDate = DateTime.Today;
            customerInDb.Bill = (float)(customerInDb.DepartureDate - customerInDb.ArrivalDate).Value.TotalDays * customerInDb.Room.Price;
            
            user.IsRenter = false;
            
            IdentityResult x = await _userManager.UpdateAsync(user);
            _context.SaveChanges();

            //return Ok();
            return Json("Oh ! at last long process finish");
        }

        /*[HttpPost]
        public IActionResult CreateNewRentals ([FromBody]CustomerDto customerDto)
        {
            var customer = _context.Customers.Single(c => c.Id == customerDto.Id);

            var room = _context.Rooms.Single(r => r.Id == customerDto.RoomId);

            
            /*if (room.ActiveRenters == room.Capacity)
                return BadRequest("Room is full");
            

            room.ActiveRenters++;
            var rental = new Rental
            {
                Customer = customer,
                Room = room,
                ArivalDate = customer.ArrivalDate,
                DepartureDate = DateTime.Now
            };

            _context.Rentals.Add(rental);

            _context.SaveChanges();

            return Ok();
        }*/
    }
}
