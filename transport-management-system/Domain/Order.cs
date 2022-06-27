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
            RouteId = reader.GetInt32("RouteId");
            Created = reader.GetDateTime("Created");
            NumberOfCourses = reader.GetInt32("NumberOfCourses");
            Price = reader.GetDecimal("Price");
            CompanyId = reader.GetInt32("CompanyId");
            StatusId = reader.GetInt32("StatusId");
        }

        public Order(int routeId, int numberOfCourses, decimal price, int companyId)
        {
            RouteId = routeId;
            Created = DateTime.Now;
            NumberOfCourses = numberOfCourses;
            Price = price;
            CompanyId = companyId;
            StatusId = (int)OrderStatus.Pending;
        }

        public int? Id { get; set; }
        public int? RouteId { get; set; }
        public virtual Route? Route { get; set; }
        public DateTime Created { get; set; }
        public int NumberOfCourses { get; set; }
        public decimal Price { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        public int StatusId { get; set; }
    }
}