using Microsoft.Data.SqlClient;

namespace PHPetshop.Services
{
    public class DbService : IDbService
    {
        private SqlConnection connection;
        public DbService(string connectionString)
        {
            //_connectionString = connectionString;
            connection = new SqlConnection(connectionString);
            try {
                connection.Open();  //testando conexão com o banco.
                connection.Close();
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
