using MySql.Data.MySqlClient;
using transport_management_system.Database;
using transport_management_system.Domain;

namespace transport_management_system.Infrastructure.SQL
{
    public class MySqlUpdateQuery
    {
        private readonly string _query;
        public MySqlUpdateQuery(string query)
        {
            _query = query;
        }

        public void ExecuteQuery()
        {
            using var connection = DbConnectionService.Instance.Connection;

            var command = new MySqlCommand(_query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
