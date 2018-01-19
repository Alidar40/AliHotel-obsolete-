using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testMVC.Models;
using testMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using testMVC.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace testMVC.Controllers
{
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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var rooms = _context.Rooms.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                Rooms = rooms
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer
                };

                return View("CustomerForm", viewModel);
            }
            if (customer.Id == null)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.DepartureDate = customer.DepartureDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ViewResult Index()
        {
            //var customers = GetCustomers();

            //var customers = _context.Customers.ToList();
            var customers = _context.Customers.Include(c => c.Room).ToList().Select(Mapper.Map<Customer, CustomerDto>);
            /*foreach (var c in _context.Customers)
            {
                foreach (var r in _context.Rooms)
                {
                    if(c.RoomId == r.Id)
                    {
                        c.Room = r;
                    }
                }
            }*/

            //return View(customers);
            return View();
        }

        public IActionResult Details(int id)
        {
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            
            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer
            };

            return View("CustomerForm", viewModel);
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