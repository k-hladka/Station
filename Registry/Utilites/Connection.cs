using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Registry.Models;

namespace Registry.Utilites
{
    public class Connection
    {
        public static IIncludableQueryable<Schedule, City> connectionAllClasses(StationContext context)
        {
            var infoTicket = context.Schedules
                        .Include(c => c.TransportInfoNavigation)
                        .Include(k => k.TypeTransport)
                        .Include(l => l.TransportInfoNavigation.FromCity)
                        .Include(l => l.TransportInfoNavigation.ToCity);
            return infoTicket;
        }
        public static Schedule? connectionWhereTransportNumber(StationContext context, int numberTransport)
        {
            var infoTicket = context.Schedules
                        .Where(c => c.Id == numberTransport)
                        .Include(f => f.TransportInfoNavigation)
                        .Include(k => k.TypeTransport)
                        .Include(l => l.TransportInfoNavigation.FromCity)
                        .Include(l => l.TransportInfoNavigation.ToCity)
                        .FirstOrDefault();
            return infoTicket;
        }
        public static decimal getPriceOnSeats(Schedule priceOneTicket, decimal count_of_seats)
        {
            return priceOneTicket.TransportInfoNavigation.Price * count_of_seats;
        }
    }
}
