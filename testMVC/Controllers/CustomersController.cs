using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using testMVC.Models;
using testMVC.Models.AccountViewModels;
using testMVC.Models.ManageViewModels;
using testMVC.ViewModels;
using testMVC.Dtos;
using AutoMapper;

namespace testMVC.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomersController(UserManager<ApplicationUser> userManager)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnet-identity-BB984385-CB7D-4F7D-829B-DEAF83487460;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            _context = new ApplicationDbContext(options);
            _userManager = userManager;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
                customerInDb.DepartureDate = customer.DepartureDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        [Authorize(Roles = "admin")]
        public ViewResult Ledger()
        {
            var customers = _context.Customers.Include(c => c.Room).ToList();
            
            return View("Ledger");
            
        }

        [Authorize(Roles = "admin")]
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.Room).Where(c => c.IsClosed == false).ToList();
            
            return View("List");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Details(string id)
        {
            var customer = _context.Customers.Single(c => c.Id == id);

            if (customer == null)
                return NotFound();
            
            ApplicationUser user = _userManager.FindByIdAsync(customer.UserId).Result;

            return View("Details", user);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            
            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                Rooms = _context.Rooms.ToList()
            };
            
            return View("CustomerForm", viewModel);
        }
    }
}