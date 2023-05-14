using Microsoft.Data.SqlClient;
using PHPetshop.Models;
using System.Text;
using System.Data;

namespace PHPetshop.Services.Persistence.Repositories
{
    public class UsuarioRepository : Repository
    {
        private SqlParameter Email { get; set; }
        private SqlParameter Senha { get; set; }
        private SqlParameter Telefone { get; set; }
        private SqlParameter Nome { get; set; }
        private SqlParameter Cargo { get; set; }
        private SqlParameter EmailConfirmado { get; set; }
        private EnderecoRepository Endereco { get; }

        public UsuarioRepository(SqlConnection conn) : base(conn)
        {
            Email = new SqlParameter("@email", SqlDbType.VarChar);
            Senha = new SqlParameter("@senha", SqlDbType.VarChar);
            Telefone = new SqlParameter("@telefone", SqlDbType.VarChar);
            Nome = new SqlParameter("@nome", SqlDbType.VarChar);
            Cargo = new SqlParameter("@cargo", SqlDbType.Int);
            EmailConfirmado = new SqlParameter("@emailConfirmado", SqlDbType.Bit);
            Endereco = new EnderecoRepository(Connection);
        }

        public void Insert(Usuario user)
        {
            CommandBuilder = new StringBuilder("INSERT INTO Usuarios(email_login, senha, telefone, nome, cargo)" +
                "VALUES (@email, @senha, @telefone, @nome, @cargo);");
            SqlCommand command = new SqlCommand();

            Email.Value = user.Email;
            Senha.Value = user.Senha;
            Telefone.Value = user.Telefone;
            Nome.Value = user.Nome;
            Cargo.Value = user.Cargo;

            command.Parameters.Add(Email);
            command.Parameters.Add(Senha);
            command.Parameters.Add(Telefone);
            command.Parameters.Add(Nome);
            command.Parameters.Add(Cargo);

            ExecuteNonQuery(command);
            Endereco.Insert(user.Email, user.Endereco);
        }

        public Usuario ObterPorEmail(string email)
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader DataReader;
            Usuario user;

            CommandBuilder = new StringBuilder(
                "SELECT * FROM Usuarios, Enderecos " +
                "WHERE Usuarios.email_login LIKE @email " +
                "AND Usuarios.email_login LIKE Enderecos.login_usuario;");
            Email.Value = email;
            command.Parameters.Add(Email);
            var result = ExecuteQuery(command).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            return new Usuario
            {
                Email = (string)result["email_login"],
                Senha = (string)result["senha"],
                Nome = (string)result["nome"],
                Telefone = (string)result["telefone"],
                Cargo = (Cargo)result["cargo"],
                Endereco = new Endereco
                {
                    Logradouro = (string)result["logradouro"],
                    Numero = (int)result["numero"],
                    Complemento = (string)result["complemento"],
                    Cep = (string)result["cep"],
                    Cidade = (string)result["cidade"],
                    UF = (string)result["uf"]
                }
            };
        }
    }
}
