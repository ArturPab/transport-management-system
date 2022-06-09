namespace transport_management_system.Domain
{
    public class Route
    {
        public int Id { get; set; }
        public float RouteLength { get; set; }
        public int ToAddressId { get; set; }
        public Address ToAddress { get; set; }
        public int FromAddressId { get; set; }
        public Address FromAddress { get; set; }
    }
}