using System;
using transport_management_system.Common.Lookups;
using transport_management_system.MVVM.Model;

namespace transport_management_system.Domain.DTO
{
    internal class OrderDTO
    {
        public OrderDTO()
        {
        }

        public OrderDTO(Order order)
        {
            Id = order.Id;
            Created = order.Created;
            NumberOfCourses = order.NumberOfCourses;
            Price = order.Price;
            OrderStatusId = order.StatusId;
            RouteId = (int)order.RouteId;
            RouteDTO = new RouteDTO(order.Route);
            CompanyId = (int)order.CompanyId;
            Company = order.Company;
        }

        public int? Id { get; set; }
        public DateTime? Created { get; set; }
        public int NumberOfCourses { get; set; }
        public decimal Price { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatusName => OrderStatusId != 0 ? OrderStatusLookup.Descriptions[OrderStatusId] : OrderStatusLookup.Descriptions[(int)CarStatus.Available];
        public int RouteId { get; set; }
        public RouteDTO? RouteDTO { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
