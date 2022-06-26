using transport_management_system.Common.Lookups;

namespace transport_management_system.Domain.DTO
{
    internal class CarDTO
    {
        public int? Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public float Payload { get; set; }
        public string ProductionYear { get; set; }
        public string Vin { get; set; }
        public int StatusId { get; set; }
        public string Status => StatusId != 0 ? CarStatusLookup.Descriptions[StatusId] : CarStatusLookup.Descriptions[(int)CarStatus.Available];

        public CarDTO(Car car)
        {
            Id = car.Id;
            Mark = car.Mark;
            Model = car.Model;
            Payload = car.Payload;
            ProductionYear = car.ProductionYear;
            Vin = car.Vin;
            StatusId = car.StatusId;
        }

        public CarDTO()
        {
        }
    }
}
