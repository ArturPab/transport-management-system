using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;
using transport_management_system.MVVM.Model;

namespace transport_management_system.Infrastructure.Domain
{
    public class RouteRepository
    {
        private static RouteRepository? _instance;
        public static RouteRepository Instance => _instance ??= new RouteRepository();
        private const string TableName = "Route";

        #region SELECT

        public IEnumerable<Route> GetAllRoutes()
        {
            var builder = new MySqlSelectQueryBuilder();
            var routes = builder.SelectAllProperties<Route>()
                .From(TableName)
                .Build()
                .ExecuteQuery<Route>();

            foreach (var route in routes)
            {
                route.FromAddress = AddressRepository.Instance.GetAddress(route.FromAddressId);
                route.ToAddress = AddressRepository.Instance.GetAddress(route.ToAddressId);
            }

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

            route.FromAddress = AddressRepository.Instance.GetAddress(route.FromAddressId);
            route.ToAddress = AddressRepository.Instance.GetAddress(route.ToAddressId);

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
            var builder = new MySqlSelectQueryBuilder();
            var orders = builder.SelectAllProperties<Order>()
                .From("Order")
                .Where("RouteId", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery<Order>();

            if (orders.Any())
            {
                MessageBox.Show("Nie można usunąć firmy dla której wykonywane jest zlecenie");
                return;
            }

            var route = GetRoute(id);

            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, id).Build().ExecuteQuery();

            AddressRepository.Instance.RemoveAddress(route.FromAddressId);
            AddressRepository.Instance.RemoveAddress(route.ToAddressId);
        }

        public void RemoveRoute(Route route)
        {
            if (route.Id == null)
                throw new ArgumentException("Route has no Id");

            var builder = new MySqlSelectQueryBuilder();
            var orders = builder.SelectAllProperties<Order>()
                .From("Order")
                .Where("RouteId", WhereOperators.Equal, route.Id)
                .Build()
                .ExecuteQuery<Order>();

            if (orders.Any())
            {
                MessageBox.Show("Nie można usunąć firmy dla której wykonywane jest zlecenie");
                return;
            }

            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, route.Id).Build().ExecuteQuery();

            AddressRepository.Instance.RemoveAddress(route.FromAddressId);
            AddressRepository.Instance.RemoveAddress(route.ToAddressId);
        }

        #endregion
    }
}
