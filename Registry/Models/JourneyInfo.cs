using System;
using System.Collections.Generic;

namespace Registry.Models;

public partial class JourneyInfo
{
    public int FromCityId { get; set; }

    public int ToCityId { get; set; }

    public decimal Price { get; set; }

    public int TypeTransportId { get; set; }

    public string? PassingCities { get; set; }

    public string JourneyTime { get; set; } = null!;

    public int Id { get; set; }

    public virtual City FromCity { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual City ToCity { get; set; } = null!;

    public virtual Transport TypeTransport { get; set; } = null!;
}
