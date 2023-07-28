namespace PokemonAPI.Models;

public class Stat
{
    public int Id { get; set; }
    public int base_stat { get; set; }
    public int effort { get; set; }
    public Stat2 stat { get; set; }
}