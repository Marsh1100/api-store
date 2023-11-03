using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Application.Repository;

public class DriverRepository : GenericRepository<Driver>, IDriver
{
    private readonly FormulaContext _context;

    public DriverRepository(FormulaContext context) : base(context)
    {
       _context = context;
    }
}
