using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using transport_management_system.Database;

namespace transport_management_system.Infrastructure.SQL
{
    public class MySqlSelectQuery
    {
        private readonly string _query;
        private readonly List<MySqlParameter> _parameters;

        public MySqlSelectQuery(string query, List<MySqlParameter> parameters)
        {
            _query = query;
            _parameters = parameters;
        }

        public List<T> ExecuteQuery<T>() where T : class
        {
            using var connection = DbConnectionService.Instance.Connection;

            var command = new MySqlCommand(_query, connection);

            foreach (var p in _parameters)
            {
                command.Parameters.Add(p);
            }

            connection.Open();

            var results = new List<T>();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var model = (T)Activator.CreateInstance(typeof(T), reader)!;
                results.Add(model);
            }

            connection.Close();

            return results;
        }
    }
}
