using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Driver : BaseEntity
{

    public string Name { get; set; }

    public int Age { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    public ICollection<TeamDriver> TeamDrivers{ get; set; }

}
