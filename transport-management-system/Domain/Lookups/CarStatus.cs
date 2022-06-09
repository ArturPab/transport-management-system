using System.Collections.Generic;

namespace transport_management_system.Common.Lookups
{
    public class CarStatusLookup
    {
        public static Dictionary<int, string> Descriptions = new()
        {
            {(int) CarStatus.Available, "Dostępny"},
            {(int) CarStatus.Busy, "Zajęty"},
            {(int) CarStatus.Broken, "Zepsuty"}
        };
    }
    public enum CarStatus
    {
        Available = 1,
        Busy,
        Broken,
    }
}
