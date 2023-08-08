using System.Data;

namespace PokemonAPI.Interfaces;

public interface ISqlLiteDB
{
    public Task<DataTable> ReturnData(int userId);
}