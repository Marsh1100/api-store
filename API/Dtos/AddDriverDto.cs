using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class AddDriverDto
{
    public int IdDriver { get; set; }
    public int IdTeam { get; set; }

}