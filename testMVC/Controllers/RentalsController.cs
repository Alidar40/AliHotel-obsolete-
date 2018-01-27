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
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace testMVC.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUser _user;
        private readonly UserManager<ApplicationUser> _userManager;

        public RentalsController(UserManager<ApplicationUser> userManager)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnet-identity-BB984385-CB7D-4F7D-829B-DEAF83487460;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            _context = new ApplicationDbContext(options);
            _user = new ApplicationUser();
            _userManager = userManager;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize]
        public ActionResult New()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("AdminNew", "Rentals");
            }
            var userid = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userid).Result;

            if (user.IsRenter)
            {
                return RedirectToAction("CreateAccessDenied", "Rentals");
            }

            var rooms = _context.Rooms.ToList();
            var viewModel = new NewRentalFormViewModel
            {
                Customer = new Customer(),
                Rooms = rooms
            };
            
            //viewModel.Customer.Name = user.Name;
            //viewModel.Customer.UserId = user.Id;
            //user.IsRenter = true;

            return View("RentalsForm", viewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminNew()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userid).Result;
            

            var rooms = _context.Rooms.ToList();
            var viewModel = new NewRentalFormViewModel
            {
                Customer = new Customer(),
                Rooms = rooms
            };

            //viewModel.Customer.Name = user.Name;
            //viewModel.Customer.UserId = user.Id;
            //user.IsRenter = true;

            return View("New", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(Customer customer)
        {
            /*if (!ModelState.IsValid)
            {
                var viewModel = new NewRentalFormViewModel
                {
                    Customer = customer,
                    Rooms = _context.Rooms.ToList()
                };

                return View("RentalsForm", viewModel);
            }*/

            /*if (customer.Id == null)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                //customerInDb.BirthDate = customer.BirthDate;
                customerInDb.DepartureDate = customer.DepartureDate;
            }*/

            var userid = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userid).Result;

            user.IsRenter = true;


            customer.Name = user.Name;
            customer.UserId = user.Id;
            customer.IsClosed = false;
            customer.Bill = 0;
            customer.Room = _context.Rooms.Single(r => r.Id == customer.RoomId);
            customer.Room.IsOccupied = true;
            
            _context.Customers.Add(customer);
            _context.SaveChanges();

            IdentityResult x = await _userManager.UpdateAsync(user);
            
            return RedirectToAction("CreatedSuccessfully", "Rentals");
        }

        public IActionResult DeleteConfirmation()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userid).Result;

            if (!user.IsRenter)
            {
                return RedirectToAction("DeleteAccessDenied", "Rentals");
            }

            return View();
        }

        [Authorize]
        public async Task<ActionResult> Delete()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userid).Result;
            
            var customerInDb = _context.Customers.SingleOrDefault(c => c.UserId == user.Id && c.IsClosed == false);
            var roomInDb = _context.Rooms.Single(r => r.Id == customerInDb.RoomId);

            if (customerInDb == null)
                return NotFound();
            

            customerInDb.Room = _context.Rooms.Single(r => r.Id == customerInDb.RoomId);
            roomInDb.IsOccupied = false;

            customerInDb.IsClosed = true;
            customerInDb.DepartureDate = DateTime.Today;
            customerInDb.Bill = (float)(customerInDb.DepartureDate - customerInDb.ArrivalDate).Value.TotalDays * customerInDb.Room.Price;

            user.IsRenter = false;

            IdentityResult x = await _userManager.UpdateAsync(user);
            _context.SaveChanges();

            return RedirectToAction("GetBill", "Rentals");
            //return View("RentalsForm", viewModel);
        }

        public IActionResult CreateAccessDenied()
        {
            return View();
        }

        public IActionResult DeleteAccessDenied()
        {
            return View();
        }

        public IActionResult CreatedSuccessfully()
        {
            return View();
        }

        public IActionResult GetBill()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userid).Result;

            var customerInDb = _context.Customers.SingleOrDefault(c => c.UserId == user.Id && c.IsClosed == true && c.DepartureDate == DateTime.Today);

            ViewData["Message"] = customerInDb.Bill.ToString();

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}