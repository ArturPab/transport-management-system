using System;
using MySql.Data.MySqlClient;
using transport_management_system.Common.Lookups;
using transport_management_system.Domain;

namespace transport_management_system.MVVM.Model
{
    public class Order
    {
        public Order()
        {
        }

        public Order(MySqlDataReader reader)
        {
            Id = reader.GetInt32("Id");
            Created = reader.GetDateTime("Created");
            NumberOfCourses = reader.GetInt32("NumberOfCourses");
            Price = reader.GetDecimal("Price");
            StatusId = reader.GetInt32("StatusId");
            RouteId = reader.GetInt32("RouteId");
            CompanyId = reader.GetInt32("CompanyId");
        }

        public Order(int numberOfCourses, decimal price, int routeId, int companyId)
        {
            Created = DateTime.Now;
            NumberOfCourses = numberOfCourses;
            Price = price;
            StatusId = (int) OrderStatus.Pending;
            RouteId = routeId;
            CompanyId = companyId;
        }

        public int? Id { get; set; }
        public DateTime Created { get; set; }
        public int NumberOfCourses { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public int? RouteId { get; set; }
        public virtual Route? Route { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
    }
}