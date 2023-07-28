using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Data
{
    public class AppDbDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PokemonsCapturados> PokemonsCapturados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(pokemon => pokemon.Pokemons)
                .WithOne(user => user.User)
                .HasForeignKey(pokemon => pokemon.UserId);
        }
        
    }
}
