using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PokemonAPI.Models
{
    public class User 
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Idade { get; set; }
        [Required]
        public string CPF { get; set; }
        public string Regiao { get; set; }
        public virtual ICollection<Pokemon> Pokemons { get; set; }
    }
}
