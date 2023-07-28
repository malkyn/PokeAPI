namespace PokemonAPI.Models;

public class Pokemon
{
    public int id { get; set; }
    public string name { get; set; }
    public int weight { get; set; }
    public int height { get; set; }
    public Sprites sprites { get; set; }
    public List<Stat> stats { get; set; }
    public List<Type> types { get; set; }
}