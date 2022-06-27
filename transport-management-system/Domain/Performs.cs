using MySql.Data.MySqlClient;

namespace transport_management_system.Domain
{
    public class Performs
    {
        public Performs(MySqlDataReader reader)
        {
            OrderId = reader.GetInt32("OrderId");
            DriverId = reader.GetInt32("DriverId");
        }
        public Performs(int orderId, int driverId)
        {
            OrderId = orderId;
            DriverId = driverId;
        }

        public int OrderId { get; set; }
        public int DriverId { get; set; }
    }
}
