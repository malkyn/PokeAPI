using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Data.Dto.Users;

public class CreateUserDto
{
    [Required] public string Usuario { get; set; }
    [Required] public string Senha { get; set; }
    [Required] public string Role { get; set; }
    [Required] public string Name { get; set; }
    [Required] public int Idade { get; set; }
    [Required] public string CPF { get; set; }
    [Required] public string Regiao { get; set; }
}