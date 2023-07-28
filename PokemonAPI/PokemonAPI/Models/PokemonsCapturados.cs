using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models;

public class PokemonsCapturados
{
    [Key]
    public int Id { get; set; }
    public string PokemonName { get; set; }
    public int UserId { get; set; }
} 