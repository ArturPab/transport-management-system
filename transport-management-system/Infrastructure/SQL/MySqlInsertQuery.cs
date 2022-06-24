using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using transport_management_system.Database;

namespace transport_management_system.Infrastructure.SQL
{
    public class MySqlInsertQuery<T>
    {
        private readonly string _tableName;
        private readonly List<MySqlParameter> _parameters = new();
        private int _parametersCounter;

        public MySqlInsertQuery(string tableName, T model)
        {
            _tableName = tableName;

            var properties = typeof(T).GetProperties().Where(p => !p.GetAccessors()[0].IsVirtual);
            foreach (var property in properties)
            {
                var parameterName = $"@p{_parametersCounter++}";

                var value = property.GetValue(model);
                _parameters.Add(new MySqlParameter(parameterName, value));
            }
        }

        public long ExecuteQuery()
        {
            using var connection = DbConnectionService.Instance.Connection;

            var command = new MySqlCommand(BuildQuery(), connection);

            foreach (var p in _parameters)
            {
                command.Parameters.Add(p);
            }

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            var id = command.LastInsertedId;

            return id;
        }

        private string BuildQuery()
        {
            var query = new StringBuilder();

            query.Append($"INSERT INTO {_tableName} VALUES(");

            var parametersNames = _parameters.Select(p => p.ParameterName);
            query.Append(string.Join(',', parametersNames));

            query.Append(");");

            return query.ToString();
        }
    }
}
