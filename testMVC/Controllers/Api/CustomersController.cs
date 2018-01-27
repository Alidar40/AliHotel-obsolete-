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
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testMVC.Controllers.Api
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnet-identity-BB984385-CB7D-4F7D-829B-DEAF83487460;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            _context = new ApplicationDbContext(options);
            this._context = context;
        }

        // GET /api/customers
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetCustomers()
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
                    .ToList();
            


            return Ok(customerDtos);
        }
        
        // GET /api/customers/1
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetCustomer(string id)
        {
            var customer = _context.Customers.Include(c => c.Room).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST/api/customers
        [HttpPost(Name = "CreateCustomer")]
        [Authorize(Roles = "admin")]
        public IActionResult CreateCustomer([FromBody]CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
                //throw new Exception("Model is not valid");
                //return null;
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            //return new JsonResult(customerDto);
            return CreatedAtRoute("CreateCustomer", new { id = customer.Id }, customer);
            //return Ok(customer);
        }
        
        //PUT/api/customers/1
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateCustomer(string id, [FromBody]CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);
            //customerInDb.Name = customer.Name;
            //customerInDb.BirthDate = customer.BirthDate;

            _context.SaveChanges();

            return Ok();
        }
        
        
        //DELETE/api/customers/1
        //[HttpDelete]
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteCustomer(string id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}

