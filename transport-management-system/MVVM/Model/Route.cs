namespace transport_management_system.MVVM.Model
{
    public class Route
    {
        public int Id { get; set; }
        public float RouteLength { get; set; }
        public Address? ToAddress { get; set; }
        public Address? FromAddress { get; set; }
    }
}