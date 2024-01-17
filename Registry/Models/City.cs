using System;
using System.Collections.Generic;

namespace Registry.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<JourneyInfo> JourneyInfoFromCities { get; set; } = new List<JourneyInfo>();

    public virtual ICollection<JourneyInfo> JourneyInfoToCities { get; set; } = new List<JourneyInfo>();
}
