namespace PokemonAPI.Data.Dto;

public class Users
{
    [Required] public string Name { get; set; }
    [Required] public int Idade { get; set; }
    [Required] public string CPF { get; set; }
}