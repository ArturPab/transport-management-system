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

        public List<T> ExecuteQuery<T>() where T : class
        {
            var results = new List<T>();
            using (var connection = DBConnectionService.Instance.Connection)
            {
                MySqlCommand command = new(query, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read()) {
                    var model = (T)Activator.CreateInstance(typeof(T), reader)!;
                    results.Add(model);
                }
                connection.Close();
            }
            return results;
        }
    }
}
