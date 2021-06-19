using Microsoft.AspNetCore.Mvc;
using NationalParks.DataAccess;
using NationalParks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParks.Controllers
{
    public class GuestController : Controller
    {

        public ApplicationDbContext dbContext;

        public GuestController(ApplicationDbContext context)
        {
            dbContext = context;
        }
     
        [HttpGet]
        public IActionResult Index()
        {
            List<Guest> guestList = dbContext.Guests.ToList(); 
            return View(guestList);
        }
    }
}
