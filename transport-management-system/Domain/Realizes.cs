namespace transport_management_system.Domain
{
    public class Realizes
    {
        public Realizes(int orderId, int carId)
        {
            OrderId = orderId;
            CarId = carId;
        }

        public int OrderId { get; set; }
        public int CarId { get; set; }

    }
}
