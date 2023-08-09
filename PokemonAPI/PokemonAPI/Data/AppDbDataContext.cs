using System.Text;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Data
{
    public class AppDbDataContext : DbContext
    {

        public AppDbDataContext()
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<PokemonsCapturados> PokemonsCapturados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source= C:\\Temp\\pokemonApi.db");
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(pokemon => pokemon.Pokemons)
                .WithOne(user => user.User)
                .HasForeignKey(pokemon => pokemon.UserId);
        }
        public User Get(string usuario, string senha)
        {
            return Users.FirstOrDefault(x => x.Usuario.ToLower() == usuario.ToLower() && x.Senha == senha) ?? throw new InvalidOperationException();
        }
    }
}
