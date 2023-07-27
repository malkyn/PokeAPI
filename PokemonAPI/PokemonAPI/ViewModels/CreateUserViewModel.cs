using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.ViewModels;

public class CreateUserViewModel
{
    [Required] public string Name { get; set; }
    [Required] public int Idade { get; set; }
    [Required] public string CPF { get; set; }
}