using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
