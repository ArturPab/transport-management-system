using System.Text;
using MySql.Data.MySqlClient;

namespace transport_management_system.Database.SQL.QueryBuilders
{
    public class MySqlInsertQuery
    {
        private string _tableName;

        public MySqlInsertQuery(string tableName)
        {
           
        }

        public long ExecuteQuery()
        {
            using var connection = DbConnectionService.Instance.Connection;

            var command = new MySqlCommand(BuildQuery(), connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            var id = command.LastInsertedId;
            
            return id;
        }

        private string BuildQuery()
        {
            var query = new StringBuilder();

            query.Append($"INSERT INTO {_tableName}");

            return query.ToString();
        }
    }
}
