using Microsoft.Data.SqlClient;
using System.Text;

namespace PHPetshop.Services.Persistence {
    public class Repository {
        protected readonly SqlConnection Connection;
        protected StringBuilder CommandBuilder { get; set; }

        public Repository(SqlConnection connection) {
            Connection = connection;
        }

        protected void ExecuteNonQuery(SqlCommand command) {
            command.CommandText = CommandBuilder.ToString();
            command.Connection = Connection;
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        protected List<Dictionary<string, object>> ExecuteQuery(SqlCommand command) {
            SqlDataReader DataReader;
            List<Dictionary<string, object>> Results = new List<Dictionary<string, object>>();
            Dictionary<string, object> keyValuePairs= new Dictionary<string, object>();
            object[] Row;
            command.CommandText = CommandBuilder.ToString();
            command.Connection = Connection;

            Connection.Open();
            DataReader = command.ExecuteReader();
            var schema = DataReader.GetColumnSchema();
            Row = new object[schema.Count];
            while(DataReader.Read()) {
                DataReader.GetValues(Row);
                for(int i = 0; i < schema.Count; i++) {
                    keyValuePairs.Add(schema[i].ColumnName, Row[i]);
                }
                Results.Add(keyValuePairs);
            }
            DataReader.Close();
            Connection.Close();
            return Results;
        }
        protected virtual void Clear() { }
    }
}
