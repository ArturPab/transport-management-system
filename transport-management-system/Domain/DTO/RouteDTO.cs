using System.Text;

namespace transport_management_system.Domain.DTO
{
    internal class RouteDTO
    {
        public RouteDTO()
        {
        }

        public RouteDTO(Route fetchedRoute)
        {
            Id = fetchedRoute.Id;
            RouteLength = fetchedRoute.RouteLength;
            ToAddress = fetchedRoute.ToAddress;
            FromAddress = fetchedRoute.FromAddress;
        }

        public int? Id { get; set; }
        public float RouteLength { get; set; }
        public Address? ToAddress { get; set; }
        public Address? FromAddress { get; set; }
        public string FromFullAddress
        {
            get
            {
                StringBuilder fromAddressBuilder = new();
                fromAddressBuilder.Append(FromAddress.Country);
                fromAddressBuilder.Append(", ");
                fromAddressBuilder.Append(FromAddress.PostalCode);
                fromAddressBuilder.Append(", ");
                fromAddressBuilder.Append(FromAddress.City);
                fromAddressBuilder.Append(", ");
                fromAddressBuilder.Append(FromAddress.Street);
                if (FromAddress.BuildingNumber != null)
                {
                    fromAddressBuilder.Append(", ");
                    fromAddressBuilder.Append(FromAddress.BuildingNumber);
                }

                return fromAddressBuilder.ToString();
            }
        }

        public string ToFullAddress
        {
            get
            {
                StringBuilder toAddressBuilder = new();
                toAddressBuilder.Append(ToAddress.Country);
                toAddressBuilder.Append(", ");
                toAddressBuilder.Append(ToAddress.PostalCode);
                toAddressBuilder.Append(", ");
                toAddressBuilder.Append(ToAddress.City);
                toAddressBuilder.Append(", ");
                toAddressBuilder.Append(ToAddress.Street);
                if (ToAddress.BuildingNumber != null)
                {
                    toAddressBuilder.Append(", ");
                    toAddressBuilder.Append(ToAddress.BuildingNumber);
                }

                return toAddressBuilder.ToString();
            }
        }

        public string FullRoute
        {
            get
            {
                StringBuilder toAddressBuilder = new();
                toAddressBuilder.Append(FromFullAddress);
                toAddressBuilder.Append(" - ");
                toAddressBuilder.Append(ToFullAddress);

                return toAddressBuilder.ToString();
            }
        }
    }
}
