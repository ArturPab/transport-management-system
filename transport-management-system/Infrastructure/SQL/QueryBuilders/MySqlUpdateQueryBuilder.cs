using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace transport_management_system.Infrastructure.SQL.QueryBuilders
{
    public class MySqlUpdateQueryBuilder<T>
    {
        private T _entity;
        private readonly List<MySqlParameter> _parameters = new();
        private int _parametersCounter = 0;
        private readonly string _tableName = string.Empty;
        private readonly object _id;

        public MySqlUpdateQueryBuilder(T entity)
        {
            if (entity is null)
                throw new ArgumentException("Entity can not be null");

            var idProp = typeof(T).GetProperties().FirstOrDefault(p => p.Name.Equals("Id"));

            if (idProp == null)
                throw new ArgumentException("Entity has no field Id");

            var id = idProp.GetValue(entity);

            if (id is null)
                throw new ArgumentException("Entity has empty Id field");

            _id = id;
            _entity = entity;
            _tableName = typeof(T).Name;
        }

        public MySqlUpdateQuery Build()
        {
            var query = new StringBuilder();

            query.Append($"UPDATE {_tableName} ");

            var set = new List<string>();
            var properties = typeof(T).GetProperties().Where(p => !p.GetAccessors()[0].IsVirtual && !p.Name.Equals("Id"));
            foreach (var property in properties)
            {
                var parameterName = $"@p{_parametersCounter++}";

                var value = property.GetValue(_entity);
                _parameters.Add(new MySqlParameter(parameterName, value));

                set.Add(string.Concat(property.Name, " = ", parameterName));
            }
            query.Append(" SET ");
            query.Append(string.Join(", ", set));

            _parameters.Add(new MySqlParameter("@id", _id));

            query.Append($" WHERE Id = @id");

            return new MySqlUpdateQuery(query.ToString(), _parameters);
        }
    }
}
