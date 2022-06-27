using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using transport_management_system.Core;
using transport_management_system.Domain;
using transport_management_system.Domain.DTO;
using transport_management_system.Infrastructure.Domain;

namespace transport_management_system.MVVM.Model
{
    internal class RoutesModel : ObservableObject
    {
        private ObservableCollection<RouteDTO> _routes;

        public ObservableCollection<RouteDTO> Routes
        {
            get { return _routes; }
            set
            {
                _routes = value;
                OnPropertyChanged("Routes");
            }
        }

        public RoutesModel()
        {
            SetRoutes();
        }

        private void SetRoutes()
        {
            IEnumerable<Route> routes = RouteRepository.Instance.GetAllRoutes();
            ConvertRoutesToRouteDTOs(routes);
        }

        private void ConvertRoutesToRouteDTOs(IEnumerable<Route> fetchedRoutes)
        {
            Routes = new ObservableCollection<RouteDTO>();
            if (fetchedRoutes.Any())
            {
                foreach (Route fetchedRoute in fetchedRoutes)
                {
                    Routes.Add(new RouteDTO(fetchedRoute));
                }
            }
        }

        internal void DeleteRoute(object route)
        {
            if (IsRoute(route))
            {
                int routeId = (int)((RouteDTO)route).Id;
                RouteRepository.Instance.RemoveRoute(routeId);
                AddressRepository.Instance.RemoveAddress(((RouteDTO)route).ToAddress);
                AddressRepository.Instance.RemoveAddress(((RouteDTO)route).FromAddress);
                SetRoutes();
            }
        }

        private static bool IsRoute(object route)
        {
            return route != null && route is RouteDTO;
        }
    }
}
