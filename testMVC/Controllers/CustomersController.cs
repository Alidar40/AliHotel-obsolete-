using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace testMVC.Controllers
{
    public class CustomersController : Controller
    {
        
        private ApplicationDbContext _context;

        public CustomersController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=mobilesdb;Trusted_Connection=True;")
                .Options;

            _context = new ApplicationDbContext(options);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        

        public ViewResult Index()
        {
            //var customers = GetCustomers();

            var customers = _context.Customers.ToList();

            return View(customers);
        }

        public IActionResult Details(string id)
        {
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        /*
        private IEnumerable<Customer>GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = "A1", Name = "Ivan Ivanov", Room = 1, ArrivalDate = "30.12.2017", DepartureDate =  "08.01.2018"},
                new Customer { Id = "A2", Name = "Petr Petrov", Room = 2, ArrivalDate = "31.11.2017", DepartureDate =  "20.12.2017"}
            };
        }
        */
    }
}