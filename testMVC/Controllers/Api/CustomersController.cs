using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testMVC.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testMVC.Controllers.Api
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AliHotelDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            _context = new ApplicationDbContext(options);
        }

        // GET /api/customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomer()
        {
            return _context.Customers.ToList();
        }
        
        // GET /api/customers/1
        [HttpGet("{id}")]
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return null;

            return customer;
        }

        //POST/api/customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Model is not valid");
                //return null;
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }
        
        //PUT/api/customers/1
        [HttpPut("{id}")]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new Exception("Bad request");

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new Exception("Not found");

            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;

            _context.SaveChanges();
        }
        
        //[HttpDelete("{id}")]
        //DELETE/api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new Exception("Not found");

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}

