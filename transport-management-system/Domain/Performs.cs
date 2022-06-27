namespace transport_management_system.Domain
{
    public class Performs
    {
        public Performs(int orderId, int driverId)
        {
            OrderId = orderId;
            DriverId = driverId;
        }

        public int OrderId { get; set; }
        public int DriverId { get; set; }

    }
}
