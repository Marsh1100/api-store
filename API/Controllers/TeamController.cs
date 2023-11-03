using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class TeamController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TeamController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TeamDto>>> Get()
    {
        var result = await _unitOfWork.Teams.GetAllAsync();
        return _mapper.Map<List<TeamDto>>(result);
    }


    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Team>> Post([FromBody] TeamDto dto)
    {
        var result = _mapper.Map<Team>(dto);
        this._unitOfWork.Teams.Add(result);
        await _unitOfWork.SaveAsync();


        if(result == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=result.Id}, result);
    }


    [HttpPut()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Team>> put(TeamDto dto)
    {
        if(dto == null){ return NotFound(); }
        var result = this._mapper.Map<Team>(dto);
        this._unitOfWork.Teams.Update(result);
        Console.WriteLine(await this._unitOfWork.SaveAsync());
        return result;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]


    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Teams.GetByIdAsync(id);
        if(result == null)
        {
            return NotFound();
        }
        this._unitOfWork.Teams.Remove(result);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }
   
}
