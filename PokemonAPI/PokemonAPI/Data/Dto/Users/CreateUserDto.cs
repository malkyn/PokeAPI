using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Data.Dto.Users;

public class CreateUserDto
{
    [Required] public string Nome { get; set; }
    [Required] public int Idade { get; set; }
    [Required] public string CPF { get; set; }
}