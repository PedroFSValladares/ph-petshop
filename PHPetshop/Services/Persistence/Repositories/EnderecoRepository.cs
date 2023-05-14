using Microsoft.Data.SqlClient;
using System.Data;
using PHPetshop.Models;
using System.Text;

namespace PHPetshop.Services.Persistence.Repositories
{
    public class EnderecoRepository : Repository
    {
        private SqlParameter Login { get; } = new SqlParameter("@login_usuario", SqlDbType.VarChar);
        private SqlParameter Logradouro { get; } = new SqlParameter("@logradouro", SqlDbType.VarChar);
        private SqlParameter Numero { get; set; } = new SqlParameter("@numero", SqlDbType.VarChar);
        private SqlParameter Complemento { get; } = new SqlParameter("@complemento", SqlDbType.VarChar);
        private SqlParameter Cidade { get; } = new SqlParameter("@cidade", SqlDbType.VarChar);
        private SqlParameter UF { get; } = new SqlParameter("@uf", SqlDbType.VarChar);
        private SqlParameter Cep { get; } = new SqlParameter("@cep", SqlDbType.VarChar);

        public EnderecoRepository(SqlConnection conn) : base(conn)
        {

        }

        public void Insert(string userLogin, Endereco endereco)
        {
            CommandBuilder = new StringBuilder(
                "INSERT INTO Enderecos(login_usuario,logradouro,numero,cep,cidade,uf)" +
                "VALUES(@login_usuario,@logradouro,@numero,@cep,@cidade,@uf);");
            SqlCommand command = new SqlCommand();

            if (endereco.Complemento != null)
            {
                CommandBuilder.Insert(CommandBuilder.ToString().IndexOf("numero,"), "complemento,");
                CommandBuilder.Insert(CommandBuilder.ToString().IndexOf("@numero,"), "@complemento,");
                Complemento.Value = endereco.Complemento;
                command.Parameters.Add(Complemento);
            }
            Login.Value = userLogin;
            Logradouro.Value = endereco.Logradouro;
            Numero.Value = endereco.Numero;
            Cidade.Value = endereco.Cidade;
            UF.Value = endereco.UF;
            Cep.Value = endereco.Cep;

            command.Parameters.Add(Login);
            command.Parameters.Add(Logradouro);
            command.Parameters.Add(Numero);
            command.Parameters.Add(Cidade);
            command.Parameters.Add(UF);
            command.Parameters.Add(Cep);

            ExecuteNonQuery(command);
        }

        public Endereco ObterPorEmail(string email)
        {
            return new Endereco();
        }
    }
}
