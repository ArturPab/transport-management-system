using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using transport_management_system.DB;

namespace transport_management_system.SqlBuilder
{
    public class SqlQueryResult
    {
        private readonly string query;

        public SqlQueryResult(string query)
        {
            this.query = query;
        }

        public List<T> ExecuteQuery<T>() where T : new()
        {
            var results = new List<T>();
            using (var connection = DBConnectionService.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    results.Add((T)Activator.CreateInstance(typeof(T), new object[] { reader }));
                connection.Close();
            }
            return results;
        }
    }
}
