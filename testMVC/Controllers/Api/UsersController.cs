using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace testMVC.Controllers.Api
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnet-identity-BB984385-CB7D-4F7D-829B-DEAF83487460;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            _context = new ApplicationDbContext(options);
            //this._context = context;

            _userManager = userManager;
        }

        // GET /api/users
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetUsers(string query = null)
        {
            var usersQuery = _userManager.Users;

            if (!String.IsNullOrWhiteSpace(query))
                usersQuery = usersQuery.Where(c => c.Name.Contains(query));

            var userDtos = usersQuery
                .ToList();

            return Ok(userDtos);
        }

        // GET /api/users/1
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUser(string id)
        {
            ApplicationUser user = _userManager.FindByIdAsync(id).Result;
            
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}