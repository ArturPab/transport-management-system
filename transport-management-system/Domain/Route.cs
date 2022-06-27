using MySql.Data.MySqlClient;

namespace transport_management_system.Domain
{
    public class Route
    {
        public Route()
        {
        }

        public Route(MySqlDataReader reader)
        {
            Id = reader.GetInt32("Id");
            RouteLength = reader.GetFloat("RouteLength");
            ToAddressId = reader.GetInt32("ToAddressId");
            FromAddressId = reader.GetInt32("FromAddressId");
        }

        public Route(float routeLength, int toAddressId, int fromAddress)
        {
            RouteLength = routeLength;
            ToAddressId = toAddressId;
            FromAddressId = fromAddress;
        }

        public int? Id { get; set; }
        public float RouteLength { get; set; }
        public int ToAddressId { get; set; }
        public virtual Address? ToAddress { get; set; }
        public int FromAddressId { get; set; }
        public virtual Address? FromAddress { get; set; }
    }
}