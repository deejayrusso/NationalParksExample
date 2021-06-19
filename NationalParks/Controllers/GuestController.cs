using Microsoft.AspNetCore.Http;
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


        public IActionResult Edit(int id)
        {
            List<Guest> guestList = dbContext.Guests.ToList();
            Guest guest = guestList.Single(x => x.Id == id);
            return View("AddEdit",guest);
        }
        public ActionResult Delete(int id)
        {
            List<Guest> guestList = dbContext.Guests.ToList();
            Guest guest = guestList.Single(x => x.Id == id);

            dbContext.Guests.Remove(guest);
            dbContext.SaveChanges();
            guestList = dbContext.Guests.ToList();
            return View("Index", guestList);
        }

        public ActionResult Add()
        {
            return View("AddEdit");
        }
        public IActionResult Cancel()
        {
            List<Guest> guestList = dbContext.Guests.ToList();
            return View("Index", guestList);
        }
        [HttpPost]
        public IActionResult Save(Guest guest)
        {
            if (guest.Id == 0)
                dbContext.Guests.Add(guest);
            else
                dbContext.Guests.Update(guest);
   
            dbContext.SaveChanges();
            List<Guest> guestList = dbContext.Guests.ToList();
            return View("Index", guestList);
        }
    }
}
