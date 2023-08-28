using AutoMapper;
using PokemonAPI.Data.Dto.Pokemon;
using PokemonAPI.Models;

namespace PokemonAPI.Profiles;

public class PokemonProfile : Profile
{
    public PokemonProfile()
    {
        CreateMap<CapturarPokemon, Pokemon>();
    }
}