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
            var labels = guestList.GroupBy(x => x.Park).Select(x => x.FirstOrDefault()).ToList();

            string[] ChartLabels = labels.Select(x => x.Park.ToString()).ToArray();
            ViewBag.Labels = String.Join(",", ChartLabels.Select(d => "'" + d + "'"));

            var chart = guestList.GroupBy(m => m.Park).Select(g => g.Count()).ToList();
            ViewBag.Data = String.Join(",", chart.Select(d => d));

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
            var labels = guestList.GroupBy(x => x.Park).Select(x => x.FirstOrDefault()).ToList();

            string[] ChartLabels = labels.Select(x => x.Park.ToString()).ToArray();
            ViewBag.Labels = String.Join(",", ChartLabels.Select(d => "'" + d + "'"));

            var chart = guestList.GroupBy(m => m.Park).Select(g => g.Count()).ToList();
            ViewBag.Data = String.Join(",", chart.Select(d => d));
            //         logins
            //.GroupBy(l => l.Date)
            //.Select(g => new
            //{
            //    Date = g.Key,
            //    Count = g.Select(l => l.Login).Distinct().Count()
            //});


            return View("Index", guestList);
        }

        public ActionResult Add()
        {
            return View("AddEdit");
        }
        public IActionResult Cancel()
        {
            List<Guest> guestList = dbContext.Guests.ToList();
            var labels = guestList.GroupBy(x => x.Park).Select(x => x.FirstOrDefault()).ToList();

            string[] ChartLabels = labels.Select(x => x.Park.ToString()).ToArray();
            ViewBag.Labels = String.Join(",", ChartLabels.Select(d => "'" + d + "'"));

            var chart = guestList.GroupBy(m => m.Park).Select(g => g.Count()).ToList();
            ViewBag.Data = String.Join(",", chart.Select(d => d));
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
            var labels = guestList.GroupBy(x => x.Park).Select(x => x.FirstOrDefault()).ToList();

            string[] ChartLabels = labels.Select(x => x.Park.ToString()).ToArray();
            ViewBag.Labels = String.Join(",", ChartLabels.Select(d => "'" + d + "'"));

            var chart = guestList.GroupBy(m => m.Park).Select(g => g.Count()).ToList();
            ViewBag.Data = String.Join(",", chart.Select(d => d));
            return View("Index", guestList);
        }
    }
}
