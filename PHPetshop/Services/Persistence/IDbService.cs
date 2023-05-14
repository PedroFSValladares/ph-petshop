using System.Runtime.CompilerServices;
using PHPetshop.Services.Persistence.Repositories;

namespace PHPetshop.Services.Persistence
{
    public interface IDbService
    {
        public UsuarioRepository Usuarios { get; }
    }
}
