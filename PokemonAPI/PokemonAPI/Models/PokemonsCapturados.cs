using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models;

public class PokemonsCapturados
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string PokemonName { get; set; }
    public int UserId { get; set; }
} 