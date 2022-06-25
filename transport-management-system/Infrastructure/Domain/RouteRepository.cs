using System;
using System.Collections.Generic;
using System.Linq;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;

namespace transport_management_system.Infrastructure.Domain
{
    public class RouteRepository
    {
        private const string TableName = "Route";

        #region SELECT

        public IEnumerable<Route> GetAllRoutes()
        {
            var builder = new MySqlSelectQueryBuilder();
            var routes = builder.SelectAllProperties<Route>()
                .From(TableName)
                .Build()
                .ExecuteQuery<Route>();

            return routes;
        }

        public Route GetRoute(int id)
        {
            var builder = new MySqlSelectQueryBuilder();
            var route = builder.SelectAllProperties<Route>()
                .From(TableName)
                .Where("Id", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery<Route>().FirstOrDefault();

            if (route == null)
                throw new ArgumentException($"Route with id: {id} doesn't exist");

            return route;
        }

        #endregion

        #region INSERT

        public int AddRoute(Route route)
        {
            var id = (int) new MySqlInsertQuery<Route>("Route", route).ExecuteQuery();

            route.Id = id;

            return id;
        }

        #endregion

        #region UPDATE

        public void UpdateRoute(Route route)
        {
            new MySqlUpdateQueryBuilder<Route>(route).Build().ExecuteQuery();
        }

        #endregion

        #region DELETE

        public void RemoveRoute(int id)
        {
            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, id).Build().ExecuteQuery();

        }

        public void RemoveRoute(Route route)
        {
            if (route.Id == null)
                throw new ArgumentException("Route has no Id");

            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, route.Id).Build().ExecuteQuery();
        }

        #endregion
    }
}
