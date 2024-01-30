using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Macs;
using Registry.Models;
using Registry.Utilites;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;

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
                if (Utilit.checkCityorInitial(Request.Form["from"]) && Utilit.checkCityorInitial(Request.Form["to"])
                    && Utilit.checkDate(Request.Form["date"]))
                    using (StationContext context = new StationContext())
                    {
                        var infoTicket = Connection.connectionAllClasses(context);
                        List<Schedule> buses = new List<Schedule>();
                        buses.Capacity = 10;
                        foreach (Schedule i in infoTicket)
                        {
                            if ((i.TypeTransport.Name != "інтерсіті" && i.TypeTransport.Name != "звичайний потяг") && 
                                Utilit.stringToLower(i.TransportInfoNavigation.FromCity.Name) == Utilit.stringToLower(Request.Form["from"]) &&
                                Utilit.stringToLower(i.TransportInfoNavigation.ToCity.Name) == Utilit.stringToLower(Request.Form["to"]) &&
                                DateTime.Parse(Request.Form["date"]).Day.CompareTo(i.DepartureDate.Day) == 0 &&
                                DateTime.Parse(Request.Form["date"]).Month.CompareTo(i.DepartureDate.Month) == 0 &&
                                Utilit.checkDate(Request.Form["date"], i.DepartureTime.ToString()))
                            {
                                buses.Add(i);
                            }
                        }
                        buses.Sort((prev, next) =>prev.DepartureTime.CompareTo(next.DepartureTime));
                        object[] result = new object[] { buses, Request.Form["from"], Request.Form["to"], Request.Form["date"] };
                        return View(result);
                    }
            }

            return View();
        }
        
        public IActionResult Buy()
        {

            string? numberBus = Request.Form["numberBus"];
            if (numberBus != null)
                Response.Cookies.Append("numberBus", numberBus);

            if (Utilit.chechCountOfSeats(Request.Form["count_of_seats"]))
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                result.Add("count_of_seats", Request.Form["count_of_seats"]);

                using (StationContext context = new StationContext())
                {
                    var priceOneTicket = Connection.connectionWhereTransportNumber(context, System.Convert.ToInt32(Request.Cookies["numberBus"]));
                    if(!(priceOneTicket.CountSeats > Convert.ToUInt32(Request.Form["count_of_seats"])))
                    {
                        result.Add("count_of_seats_e", $"Невірна кількість місць. Максимально можлива кількість = {priceOneTicket.CountSeats}");
                        return View(result);
                    }
                    Response.Cookies.Append("count_of_seats", Request.Form["count_of_seats"]);
                    if (priceOneTicket != null)
                        result.Add("price", Connection.getPriceOnSeats(priceOneTicket, Convert.ToDecimal(Request.Form["count_of_seats"])));
                }

                return View(result);
            }
            else if (!Request.Form["email"].IsNullOrEmpty())
            {
                string name, surname;
                Dictionary<string, object> result = new Dictionary<string, object>();

                if (!Utilit.chechEmail(Request.Form["email"]))
                    result.Add("email_e", "Некоректна пошта");
                result.Add("count_of_seats", Request.Cookies["count_of_seats"]);
                result.Add("email", Request.Form["email"]);

                bool check = true;
                int objectIndex = 3;
                for (int i=0; i< Convert.ToInt32(Request.Cookies["count_of_seats"]); i++)
                {
                    name = "name" + i;
                    surname = "surname" + i;
                    if (!Utilit.checkCityorInitial(Request.Form[name]))
                    {
                        check = false;
                        result.Add(name + "_e", "Некоректне ім'я");
                    }
                    if (!Utilit.checkCityorInitial(Request.Form[surname]))
                    {
                        check = false;
                        result.Add(surname + "_e", "Некоректне прізвище");
                    }

                    result.Add(name, Request.Form[name]);
                    result.Add(surname, Request.Form[surname]);
                }
                if (!check)
                {
                    using (StationContext context = new StationContext())
                    {
                        var priceOneTicket = Connection.connectionWhereTransportNumber(context, System.Convert.ToInt32(Request.Cookies["numberBus"]));
                        if (priceOneTicket != null)
                            result.Add("price", Connection.getPriceOnSeats(priceOneTicket, Convert.ToDecimal(Request.Cookies["count_of_seats"])));
                    }
                    return View(result);
                }
                using (StationContext context = new StationContext())
                {
                    var ticket = Connection.connectionWhereTransportNumber(context, System.Convert.ToInt32(Request.Cookies["numberBus"]));
                    if (ticket != null && ticket.CountSeats - Convert.ToInt32(Request.Cookies["count_of_seats"])>=0)
                    ticket.CountSeats -= Convert.ToInt32(Request.Cookies["count_of_seats"]);
                    context.SaveChanges();
                    StringBuilder textTicket = new StringBuilder();
                    for(int i=0, j=1; i< System.Convert.ToInt32(Request.Cookies["count_of_seats"]); i++, j++)
                    {
                        textTicket.Append("<br/><br/>" +
                            "<br/><span style='color:blue'>Квиток для пасажира №"+ j + "</span>"+ 
                            "<br/>Квиток на автобус " + "<span class='text-danger'>" + ticket.TransportInfoNavigation.FromCity.Name + 
                            "-" + ticket.TransportInfoNavigation.ToCity.Name + "</span>" +
                            "<br/>Відправлення: " + ticket.DepartureDate.ToString().Substring(0, 5) + " числа в " + ticket.DepartureTime + 
                            "<br/>Прибуття: " + ticket.ArriveDate.ToString().Substring(0, 5) + " числа в " + ticket.ArriveTime +
                            "<br/>*********************** " + 
                            "<br/>Квиток на ім'я пасажира: <span style='color:green'>" + Request.Form["name"+i] + " " + Request.Form["surname"+i] + "</span>" +
                            "<br/>Увага! В квитку не вказане місце. " +
                            "<br/><small>(Місце розсаджування довільне)</small>");
                    }
                    var email = new Email(Request.Form["email"], "Квитки", textTicket.ToString());
                }
                    return View("Success");
            }
            else
            {
                return View();
            }
        }
       
    }
}