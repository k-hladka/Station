using System;
using System.Collections.Generic;

namespace Registry.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public int TransportInfo { get; set; }

    public DateTime DepartureDate { get; set; }

    public TimeSpan DepartureTime { get; set; }

    public DateTime ArriveDate { get; set; }

    public TimeSpan ArriveTime { get; set; }

    public int TypeTransportId { get; set; }

    public int CountSeats { get; set; }

    public virtual JourneyInfo TransportInfoNavigation { get; set; } = null!;

    public virtual Transport TypeTransport { get; set; } = null!;
}
