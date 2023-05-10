using System.Runtime.CompilerServices;

namespace PHPetshop.Services.Persistence
{
    public interface IDbService
    {
        public UsuarioRepository Usuarios { get; }
    }
}
