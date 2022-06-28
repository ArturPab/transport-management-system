using MySql.Data.MySqlClient;

namespace transport_management_system.Domain
{
    public class Realizes
    {
        public Realizes(MySqlDataReader reader)
        {
            OrderId = reader.GetInt32("OrderId");
            CarId = reader.GetInt32("CarId");
        }
        public Realizes(int orderId, int carId)
        {
            OrderId = orderId;
            CarId = carId;
        }

        public int OrderId { get; set; }
        public int CarId { get; set; }

    }
}
