namespace PokemonAPI.Models;

public class PokemonResult
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public int Altura { get; set; }
    public int Peso { get; set; }
    public string Base64 { get; set; }
    public List<string> Evolucoes { get; set; }
}