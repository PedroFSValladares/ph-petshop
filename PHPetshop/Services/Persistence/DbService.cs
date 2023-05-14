using Microsoft.Data.SqlClient;
using PHPetshop.Services.Persistence.Repositories;

namespace PHPetshop.Services.Persistence
{
    public class DbService
    {
        public SqlConnection Connection { get; }
        public UsuarioRepository Usuarios { get; }
        public DbService(SqlConnection connection, UsuarioRepository usuarioRepository){
            Connection = connection;
            Usuarios = usuarioRepository;
        }
    }
}
