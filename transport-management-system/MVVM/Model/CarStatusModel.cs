using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transport_management_system.Common.Lookups;

namespace transport_management_system.MVVM.Model
{
    internal class CarStatusModel
    {
        public int StatusId { get; set; }
        public string? Status { get; set; }
    }
}
