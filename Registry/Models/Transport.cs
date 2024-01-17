using System;
using System.Collections.Generic;

namespace Registry.Models;

public partial class Transport
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CoutSeats { get; set; }

    public virtual ICollection<JourneyInfo> JourneyInfos { get; set; } = new List<JourneyInfo>();
}
