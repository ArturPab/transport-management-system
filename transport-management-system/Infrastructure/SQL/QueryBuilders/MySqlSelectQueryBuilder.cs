using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using transport_management_system.Infrastructure.SQL.Enums;

namespace transport_management_system.Infrastructure.SQL.QueryBuilders
{
    public class MySqlSelectQueryBuilder
    {
        private readonly List<string> _select = new();
        private readonly List<string> _where = new();
        private readonly List<MySqlParameter> _parameters = new();
        private int _parametersCounter = 0;

        private string _dataSource = string.Empty;

        public MySqlSelectQueryBuilder Select(params string[] columns)
        {
            if (columns is null || columns.Length == 0)
                throw new ArgumentException("Columns can not be empty");

            _select.AddRange(columns.Select(c => c.Trim()));

            return this;
        }

        public MySqlSelectQueryBuilder SelectAllProperties<T>() where T : class
        {
            var props = typeof(T).GetProperties().Where(p => !p.GetAccessors()[0].IsVirtual).ToArray();
            var cols = props.Select(p => p.Name.Trim()).ToArray();

            return Select(cols);
        }

        public MySqlSelectQueryBuilder From(string dataSource)
        {
            if (dataSource is null || dataSource.Length == 0)
                throw new ArgumentException("Data source can not be empty");

            _dataSource = dataSource;

            return this;
        }

        public MySqlSelectQueryBuilder Where(string column, WhereOperators @operator, object value)
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

        public MySqlSelectQuery Build()
        {
            var query = new StringBuilder();

            //SELECT
            query.Append("SELECT ");
            query.Append(string.Join(", ", _select));

            //FORM
            query.Append($" FROM `{_dataSource}`");

            //WHERE
            if (_where.Any())
            {
                query.Append(" WHERE ");
                query.Append(string.Join(", ", _where));
            }

            query.Append(';');

            return new MySqlSelectQuery(query.ToString(), _parameters);
        }
    }
}
