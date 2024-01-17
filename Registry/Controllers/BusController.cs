using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Registry.Models;
using Registry.Utilites;
using System.Data;
using System.Diagnostics;

namespace Registry.Controllers
{
    public class BusController : Controller
    {
        private readonly ILogger<BusController> _logger;

        public BusController(ILogger<BusController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            if (Request.Method == "POST")
            {
                if (Utilit.checkCity(Request.Form["from"]) && Utilit.checkCity(Request.Form["to"])
                    && Utilit.checkDate(Request.Form["date"]))
                    using (StationContext context = new StationContext())
                    {
                        var infoTicket = context.Schedules
                        .Include(c => c.TransportInfoNavigation)
                        .Include(k => k.TransportInfoNavigation.TypeTransport)
                        .Include(l => l.TransportInfoNavigation.FromCity)
                        .Include(l => l.TransportInfoNavigation.ToCity);

                        List<Schedule> buses = new List<Schedule>();
                        buses.Capacity = 10;
                        // infoTicket.DateTime = 22.08.2024 22:00:00
                        //2024-01-11 - parametr date
                        //12.01.2024 00:00:00 - парсинг
                        
                        var d = DateTime.Now;
                       /* do
                        {
                            d = d.Date.AddDays(1);
                            d = d.AddHours(22);
                            if (d.DayOfWeek == DayOfWeek.Thursday)
                            {

                                Console.WriteLine(d);
                                context.Schedules.Add(new Schedule() { DateTime = d, TransportInfo = 8 });
                                context.SaveChanges();
                            }

                        } while (d.Year == DateTime.Now.Year);*/
                        foreach (Schedule i in infoTicket)
                        {
                            if (Utilit.stringToLower(i.TransportInfoNavigation.FromCity.Name) == Utilit.stringToLower(Request.Form["from"]) &&
                                Utilit.stringToLower(i.TransportInfoNavigation.ToCity.Name) == Utilit.stringToLower(Request.Form["to"]) &&
                                DateTime.Parse(Request.Form["date"]).Day.CompareTo(i.DepartureDate.Day) == 0 &&
                                DateTime.Parse(Request.Form["date"]).Month.CompareTo(i.DepartureDate.Month) == 0)
                            {
                                 buses.Add(i);
                            }
                        }
                        object[]result = new object[] {buses, Request.Form["from"], Request.Form["to"], Request.Form["date"] };
                        return View(result);
                       /* foreach (Schedule i in x)
                            Console.WriteLine(i.DateTime + "\n" + i.TransportInfoNavigation.FromCity.Name + " - " +
                             *i.TransportInfoNavigation.ToCity.Name + " = " + i.TransportInfoNavigation.Price +
                             *"грн; тип транспорту та кількість міст = " + i.TransportInfoNavigation.TypeTransport.Name +
                             *" " + i.TransportInfoNavigation.TypeTransport.CoutSeats + "час подорожі = " +
                             *i.TransportInfoNavigation.JourneyTime);

                        Console.WriteLine(i.TransportInfoNavigation.PassingCities);*/
                    }
                /*Console.WriteLine(Request.Form["from"] + "\n" + Request.Form["to"] + "\n" + Request.Form["date"]);*/ }
           
            return View();
        }
    }
}