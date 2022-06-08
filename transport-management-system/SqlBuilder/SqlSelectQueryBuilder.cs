using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using transport_management_system.SqlBuilder;
using transport_management_system.SqlBuilder.Enums;

namespace transport_management_system.SQLBuilder
{
    public class SqlSelectQueryBuilder
    {
        private readonly List<string> _select = new();
        private readonly List<string> _where = new();
        private string _dataSource = string.Empty;

        public SqlSelectQueryBuilder Select(params string[] columns)
        {
            if (columns is null || columns.Length == 0)
                throw new ArgumentException("Columns can not be empty");

            _select.AddRange(columns.Select(c => c.Trim()));

            return this;
        }

        public SqlSelectQueryBuilder SelectAllProperties<T>() where T : class
        {
            var props = typeof(T).GetProperties();
            var cols = props.Select(p => p.Name.Trim()).ToArray();

            return Select(cols);
        }

        public SqlSelectQueryBuilder From(string dataSource)
        {
            if (dataSource is null || dataSource.Length == 0)
                throw new ArgumentException("Data source can not be empty");

            _dataSource = dataSource;

            return this;
        }

        public SqlSelectQueryBuilder Where(string column, WhereOperators @operator, object value)
        {
            if (column is null || column.Length == 0)
                throw new ArgumentException("Column can not be empty");

            if (value is null)
                throw new ArgumentException("Value can not be empty");

            var condition = string.Concat(column, GetSqlWhereOperatorString(@operator), value.ToString());
            _where.Add(condition);

            return this;
        }

        private string GetSqlWhereOperatorString(WhereOperators @operator) => @operator switch
        {
            WhereOperators.Equal => " = ",
            WhereOperators.GreaterThan => " > ",
            WhereOperators.LessThan => " < ",
            WhereOperators.GreaterThanOrEqual => " => ",
            WhereOperators.LessThanOrEqual => " <= ",
            WhereOperators.NotEqual => " <> ",
            _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
        };

        public SqlQueryResult Build()
        {
            var query = new StringBuilder();

            //SELECT
            query.Append("SELECT ");
            query.Append(string.Join(", ", _select));

            //FORM
            query.Append($" FROM {_dataSource}");

            //WHERE
            if (_where.Any())
            {
                query.Append(" WHERE ");
                query.Append(string.Join(", ", _where));
            }

            query.Append(';');

            Reset();

            return new SqlQueryResult(query.ToString());
        }

        private void Reset()
        {
            _select.Clear();
            _dataSource = string.Empty;
            _where.Clear();
        }
    }
}
