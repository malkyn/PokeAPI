using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PokemonAPI.Models
{
    public class User 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }
        public string Regiao { get; set; }
        public int NumeroDePokemon { get; set; }
        public bool Done { get; set; }
        public int PokemonId { get; set; }
        public virtual ICollection<Pokemon> Pokemons { get; set; }
    }
}
