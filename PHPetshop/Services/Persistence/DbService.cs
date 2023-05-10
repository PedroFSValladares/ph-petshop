using Microsoft.Data.SqlClient;

namespace PHPetshop.Services.Persistence
{
    public class DbService : IDbService
    {
        private SqlConnection connection;
        public UsuarioRepository Usuarios { get; }
        public DbService(string connectionString){
            connection = new SqlConnection(connectionString);
            Usuarios = new UsuarioRepository(connection);
            try
            {
                connection.Open();  //testando conexão com o banco.
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}
