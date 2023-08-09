using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Data.Dto.Users;

public class LoginUserDto
{
    [Required]
    public string Usuario { get; set; }
    [Required]
    public string Senha { get; set; }
}