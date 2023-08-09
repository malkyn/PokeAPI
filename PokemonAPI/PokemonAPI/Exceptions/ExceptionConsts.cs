namespace PokemonAPI.Exceptions;

public struct ExceptionConsts
{
    private const string Default = "Exception:";

    public struct Pokemon
    {
        public const string FalhaCaptura = $"{Default}Falha na captura";
        public const string PokemonNaoEncontrado = $"{Default}Pokemon não encontrado";
    }

    public struct Users
    {
        public const string UsuarioNaoEncontrado = $"{Default}Usuário não encontrado";
    }
}