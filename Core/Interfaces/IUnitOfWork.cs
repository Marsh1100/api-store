using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Interfaces;

public interface IUnitOfWork
{
    IDriver Drivers { get; }
    ITeam Teams { get; }

    Task<int> SaveAsync();
}
