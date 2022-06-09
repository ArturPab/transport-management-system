using System.Collections.Generic;

namespace transport_management_system.Common.Lookups;

public class OrderStatusLookup
{
    public static Dictionary<int, string> Descriptions = new()
    {
        { (int)OrderStatus.Pending, "Oczekujące" },
        { (int)OrderStatus.InProgress, "W toku" },
        { (int)OrderStatus.Done, "Wykonane" }
    };
}
public enum OrderStatus
{
    Pending,
    InProgress,
    Done,
}
