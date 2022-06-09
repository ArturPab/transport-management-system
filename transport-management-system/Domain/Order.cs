using System;
using transport_management_system.Domain;

namespace transport_management_system.MVVM.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int NumberOfCourses { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public int RouteId { get; set; }
        public virtual Route Route { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}