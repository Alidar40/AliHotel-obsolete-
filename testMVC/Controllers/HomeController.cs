using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testMVC.Models;

namespace testMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Rooms()
        {
            //ViewData["Message"] = "Rooms";

            return View();
        }

        public IActionResult Customers()
        {

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "The Best Hotel";

            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
