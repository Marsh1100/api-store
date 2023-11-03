using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Entities;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    public ICollection<TeamDriver> TeamDrivers{ get; set; }
    
}
