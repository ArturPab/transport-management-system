using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using transport_management_system.Infrastructure.SQL.Enums;

namespace transport_management_system.Infrastructure.SQL.QueryBuilders
{
    public class MySqlDeleteQueryBuilder
    {
        private readonly List<string> _where = new();
        private readonly List<MySqlParameter> _parameters = new();
        private int _parametersCounter = 0;
        private string _tableName = string.Empty;

        public MySqlDeleteQueryBuilder From(string tableName)
        {
            if (tableName is null || tableName.Length == 0)
                throw new ArgumentException("Data source can not be empty");

            _tableName = tableName;

            return this;
        }

        public MySqlDeleteQueryBuilder Where(string column, WhereOperators @operator, object value)
        {
            if (column is null || column.Length == 0)
                throw new ArgumentException("Column can not be empty");

            if (value is null)
                throw new ArgumentException("Value can not be empty");

            var parameterName = $"@p{_parametersCounter++}";
            _parameters.Add(new MySqlParameter(parameterName, value));

            var condition = string.Concat(column, @operator.GetSqlWhereOperatorString(), parameterName);
            _where.Add(condition);

            return this;
        }

        public MySqlDeleteQuery Build()
        {
            var query = new StringBuilder();

            query.Append($"DELETE FROM {_tableName}");
            
            if (_where.Any())
            {
                query.Append(" WHERE ");
                query.Append(string.Join(", ", _where));
            }

            query.Append(';');

            return new MySqlDeleteQuery(query.ToString(), _parameters);
        }
    }
}
