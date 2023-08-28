using System.Data;
using Microsoft.Data.Sqlite;
using PokemonAPI.Interfaces;

namespace PokemonAPI.Data.Database;

public class SqlLiteDB : ISqlLiteDB
{
    private readonly IConfiguration _configuration;

    public SqlLiteDB(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<DataTable> ReturnData(int userId)
    {
        const string query = "SELECT PokemonName FROM PokemonsCapturados WHERE UserId = @Vallue";
        await using (SqliteConnection connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            DataTable dt = new DataTable();

            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Vallue", userId);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
    }
}