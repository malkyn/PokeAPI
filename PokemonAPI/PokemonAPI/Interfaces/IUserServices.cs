using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Data.Dto.Users;

namespace PokemonAPI.Interfaces;

public interface IUserServices
{
    public Task<ReadUserDto> GetUser(int id);
    public Task<ReadUserDto> RegisterUser(CreateUserDto userDto);
}