using AutoMapper;
using PokemonAPI.Data.Dto.Users;
using PokemonAPI.Models;

namespace PokemonAPI.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<User, ReadUserDto>();
    }
}