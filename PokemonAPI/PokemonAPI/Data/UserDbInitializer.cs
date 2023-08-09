using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Data;

public class UserDbInitializer
{
    public static void Initializer(IServiceProvider serviceProvider)
    {
        using (var context =
               new AppDbDataContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbDataContext>>()))
        {
            if (context.Users.Any())
            {
                return;
            }
            
            context.Users.Add(
                new User
                {
                    Id = 1,
                    Usuario = "marcosvini",
                    Senha = "parli",
                    Role = "admin",
                    Name = "Marcos Vinicius",
                    Idade = 25,
                    CPF = "47586221839",
                    Regiao = "Hoenn"
                }
            );
            context.SaveChanges();
        }
    }
}