using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Application.Repository;

public class TeamRepository : GenericRepository<Team>, ITeam
{
    private readonly FormulaContext _context;

    public TeamRepository(FormulaContext context) : base(context)
    {
       _context = context;
    }
}
