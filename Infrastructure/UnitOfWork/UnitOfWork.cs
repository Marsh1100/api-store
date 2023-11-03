using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Core.Interfaces;
using Infrastructure.Data;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly FormulaContext _context;

    private ITeam _teams;
    private IDriver _drivers;

    public UnitOfWork(FormulaContext context)
    {
        _context = context;
    }
    public ITeam Teams
    {
         get
        {
            if (_teams == null)
            {
                _teams = new TeamRepository(_context);
            }
            return _teams;
        }
    }
    public IDriver Drivers
    {
        get
        {
            if (_drivers == null)
            {
                _drivers = new DriverRepository(_context);
            }
            return _drivers;
        }
    }
    
    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}