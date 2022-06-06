namespace transport_management_system.MVVM.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string? Mark { get; set; }
        public string? Model { get; set; }
        public float? Payload { get; set; }
        public string? ProductionYear { get; set; }
        public string? Vin { get; set; }
        public int StatusId { get; set; }
    }
}