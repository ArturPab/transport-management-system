using System;

namespace transport_management_system.MVVM.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created { get; set; }
        public int NumberOfCourses { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public Route? Route { get; set; }
        public Company? Company { get; set; }
    }
}