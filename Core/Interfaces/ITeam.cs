using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces;

public interface ITeam : IGenericRepository<Team>
{
    Task<string> AddDriver(int idDriver, int idTeam);
}
