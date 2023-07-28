namespace PokemonAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }
        public string Regiao { get; set; }
        public List<string> Pokemons { get; set; }
        public int NumeroDePokemon { get; set; }
        public bool Done { get; set; }
    }
}
