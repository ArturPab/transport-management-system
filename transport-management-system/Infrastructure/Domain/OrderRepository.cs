using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;
using transport_management_system.MVVM.Model;

namespace transport_management_system.Infrastructure.Domain
{
    public class OrderRepository
    {
        private static OrderRepository? _instance;
        public static OrderRepository Instance => _instance ??= new OrderRepository();
        private const string TableName = "Order";

        #region SELECT

        public IEnumerable<Order> GetAllOrders()
        {
            var builder = new MySqlSelectQueryBuilder();
            var orders = builder.SelectAllProperties<Order>()
                .From(TableName)
                .Build()
                .ExecuteQuery<Order>();

            foreach(var order in orders)
            {
                order.Company = CompanyRepository.Instance.GetCompany(order.CompanyId.Value);
                order.Route = RouteRepository.Instance.GetRoute(order.RouteId.Value);
            }

            return orders;
        }

        public Order GetOrder(int id)
        {
            var builder = new MySqlSelectQueryBuilder();
            var order = builder.SelectAllProperties<Order>()
                .From(TableName)
                .Where("Id", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery<Order>().FirstOrDefault();

            if (order == null)
                throw new ArgumentException($"Order with id: {id} doesn't exist");

            order.Company = CompanyRepository.Instance.GetCompany(order.CompanyId.Value);
            order.Route = RouteRepository.Instance.GetRoute(order.RouteId.Value);

            return order;
        }

        #endregion

        #region INSERT

        public int AddOrder(Order order)
        {
            var id = (int) new MySqlInsertQuery<Order>("Order", order).ExecuteQuery();

            order.Id = id;

            return id;
        }

        #endregion

        #region UPDATE

        public void UpdateOrder(Order order)
        {
            new MySqlUpdateQueryBuilder<Order>(order).Build().ExecuteQuery();
        }

        #endregion

        #region DELETE

        public void RemoveOrder(int id)
        {
            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, id).Build().ExecuteQuery();

        }

        public void RemoveOrder(Order order)
        {
            if (order.Id == null)
                throw new ArgumentException("Order has no Id");

            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, order.Id).Build().ExecuteQuery();
        }

        #endregion

        #region Assigning

        public void AssignDriversToOrder(Order order, List<Driver> drivers)
        {
            new MySqlDeleteQueryBuilder().From("performs").Where("OrderId", WhereOperators.Equal, order.Id.Value).Build().ExecuteQuery();

            foreach (var driver in drivers)
            {
                new MySqlInsertQuery<Performs>("performs", new Performs(order.Id.Value, driver.Id.Value)).ExecuteQuery();
            }
        }

        public List<Driver> GetAssignDriversForOrder(Order order)
        {
            var performs = new MySqlSelectQueryBuilder().SelectAllProperties<Performs>()
                .From("performs")
                .Where("OrderId", WhereOperators.Equal, order.Id.Value)
                .Build()
                .ExecuteQuery<Performs>();

            var drivers = performs.Select(performs => DriverRepository.Instance.GetDriver(performs.DriverId)).ToList();

            return drivers;
        }

        public void AssignCarsToOrder(Order order, List<Car> cars)
        {
            new MySqlDeleteQueryBuilder().From("realizes").Where("OrderId", WhereOperators.Equal, order.Id.Value).Build().ExecuteQuery();

            foreach (var car in cars)
            {
                new MySqlInsertQuery<Realizes>("realizes", new Realizes(order.Id.Value, car.Id.Value)).ExecuteQuery();
            }
        }

        public List<Car> GetAssignCarsForOrder(Order order)
        {
            var realizes = new MySqlSelectQueryBuilder().SelectAllProperties<Realizes>()
                .From("realizes")
                .Where("OrderId", WhereOperators.Equal, order.Id.Value)
                .Build()
                .ExecuteQuery<Realizes>();

            var cars = realizes.Select(realize => CarRepository.Instance.GetCar(realize.CarId)).ToList();

            return cars;
        }

        #endregion
    }
}
