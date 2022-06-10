using System;
using System.Text;
using transport_management_system.Domain;

namespace transport_management_system.Infrastructure.SQL.QueryBuilders
{
    public class MySqlUpdateQueryBuilder<T>
    {
        private T _entity;

        public MySqlUpdateQueryBuilder(T entity)
        {
            if (entity is null)
                throw new ArgumentException("Entity can not be null");

            _entity = entity;
        }

        public MySqlUpdateQuery Build()
        {
            var query = new StringBuilder();

            query.Append($"UPDATE {typeof(T).Name} ");

            var type = _entity.GetType();

            query.Append("SET ");
            
            query.Append($"WHERE Id = {""}");

            return new MySqlUpdateQuery(query.ToString());
        }
    }
}
