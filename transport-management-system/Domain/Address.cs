using MySql.Data.MySqlClient;

namespace transport_management_system.Domain
{
    public class Address
    {
        public Address()
        {
        }

        public Address(MySqlDataReader reader)
        {
            Id = reader.GetInt32("Id");
            Country = reader.GetString("Country");
            PostalCode = reader.GetString("PostalCode");
            City = reader.GetString("City");
            Street = reader.GetString("Street");
            BuildingNumber = reader.GetString("BuildingNumber");

        }

        public Address(int id, string country, string postalCode, string city, string street, string buildingNumber)
        {
            Id = id;
            Country = country;
            PostalCode = postalCode;
            City = city; 
            Street = street;
            BuildingNumber = buildingNumber;
        }

        public int? Id { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? BuildingNumber { get; set; }
    }
}