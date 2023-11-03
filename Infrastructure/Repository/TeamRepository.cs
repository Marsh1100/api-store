using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;

public class TeamRepository : GenericRepository<Team>, ITeam
{
    private readonly FormulaContext _context;

    public TeamRepository(FormulaContext context) : base(context)
    {
       _context = context;
    }

    public async Task<string> AddDriver(int idDriver, int idTeam)
    {
        var existDriver = await _context.Drivers.Where(s=> s.Id == idDriver).FirstAsync();
        var existTeam =await _context.Teams.Where(s=> s.Id == idTeam).FirstAsync();

        if(existDriver != null && existTeam != null)
        {
            _context.TeamDrivers.Add(new TeamDriver{
                IdDriver = idDriver,
                IdTeam = idTeam
            });

            await _context.SaveChangesAsync();  
            return $"Se ha agregado el conductor {existDriver.Name} al equipo {existTeam.Name}.";
        }
        return "El id del conductor o equipo no existe, verificar";

    }
}
