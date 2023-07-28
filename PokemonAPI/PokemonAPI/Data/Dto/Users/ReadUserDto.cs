using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Data.Dto.Users;

public class ReadUserDto
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Idade { get; set; }
    public string CPF { get; set; }
    public string Regiao { get; set; }
}