using System.Collections.Generic;
using MySql.Data.MySqlClient;
using transport_management_system.Database;
using transport_management_system.Domain;

namespace transport_management_system.Infrastructure.SQL
{
    public class MySqlUpdateQuery
    {
        private readonly string _query;
        private readonly List<MySqlParameter> _parameters;

        public MySqlUpdateQuery(string query, List<MySqlParameter> parameters)
        {
            _query = query;
            _parameters = parameters;
        }

        public void ExecuteQuery()
        {
            using var connection = DbConnectionService.Instance.Connection;

            var command = new MySqlCommand(_query, connection);

            foreach (var p in _parameters)
            {
                command.Parameters.Add(p);
            }

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
