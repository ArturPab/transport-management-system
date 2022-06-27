using MySql.Data.MySqlClient;
using transport_management_system.Common.Lookups;

namespace transport_management_system.Domain;

public class Car
{
    public Car()
    {
    }

    public Car(MySqlDataReader reader)
    {
        Id = reader.GetInt32("Id");
        Mark = reader.GetString("Mark");
        Model = reader.GetString("Model");
        Payload = reader.GetFloat("Payload");
        ProductionYear = reader.GetString("ProductionYear");
        Vin = reader.GetString("Vin");
        StatusId = reader.GetInt32("StatusId");
    }

    public Car(string mark, string model, float payload, string productionYear, string vin)
    {
        Mark = mark;
        Model = model;
        Payload = payload;
        ProductionYear = productionYear;
        Vin = vin;
        StatusId = (int) CarStatus.Available;
    }

    public int? Id { get; set; }
    public string Mark { get; set; }
    public string Model { get; set; }
    public float Payload { get; set; }
    public string ProductionYear { get; set; }
    public string Vin { get; set; }
    public int StatusId { get; set; }
    public virtual string FullName => Mark + " " + Model + " " + Payload + "kg";
    public virtual bool IsSelected { get; set; }
}