using System.Collections.Generic;
using System.Windows.Input;
using transport_management_system.Core;
using transport_management_system.Domain;
using transport_management_system.Domain.DTO;
using transport_management_system.Infrastructure.Domain;

namespace transport_management_system.MVVM.ViewModel
{
    internal class RoutesFormViewModel
    {
        private bool _isUpdateForm = false;
        private NavigationViewModel SelectedViewModel { get; set; }
        public RouteDTO Route { get; set; }

        private ICommand _updateRouteTableCommand;
        public ICommand UpdateRouteTableCommand
        {
            get
            {
                if (_updateRouteTableCommand == null)
                {
                    if (_isUpdateForm)
                    {
                        _updateRouteTableCommand = new RelayCommand(UpdateRoute);
                    }
                    else
                    {
                        _updateRouteTableCommand = new RelayCommand(AddRoute);
                    }
                }
                return _updateRouteTableCommand;
            }
            set
            {
                _updateRouteTableCommand = value;
            }
        }

        public RoutesFormViewModel(NavigationViewModel selectedViewModel, object route)
        {
            SetRoute(route);
            SelectedViewModel = selectedViewModel;
        }

        private void SetRoute(object route)
        {
            if (IsRoute(route))
            {
                Route = (RouteDTO)route;
                _isUpdateForm = true;
            }
            else
            {
                Route = new RouteDTO();
                Route.FromAddress = new Address();
                Route.ToAddress = new Address();
                _isUpdateForm = false;
            }
        }

        private static bool IsRoute(object route)
        {
            return route != null && route is RouteDTO;
        }

        private void AddRoute(object obj)
        {
            Route route = CreateRoute();
            AddressRepository.Instance.AddAddress(route.FromAddress);
            AddressRepository.Instance.AddAddress(route.ToAddress);
            IEnumerable<Address> fetchedAddresses = AddressRepository.Instance.GetAllAddress();
            List<Address> fromToAddresses = GetFromToAddresses(fetchedAddresses);
            route.FromAddressId = (int)fromToAddresses[0].Id;
            route.ToAddressId = (int)fromToAddresses[1].Id;
            RouteRepository.Instance.AddRoute(route);
            SelectedViewModel.SelectedViewModel = new RoutesViewModel(SelectedViewModel);
        }

        private List<Address> GetFromToAddresses(IEnumerable<Address> fetchedAddresses)
        {
            List<Address> fetchedAddressesList = new();
            foreach (Address fetchedAddress in fetchedAddresses)
            {
                fetchedAddressesList.Add(fetchedAddress);
            }

            List<Address> fromToAddresses = new()
            {
                fetchedAddressesList[^1],
                fetchedAddressesList[^2]
            };

            return fromToAddresses;
        }

        private Route CreateRoute()
        {
            Route route = new()
            {
                Id = GetRouteId(),
                RouteLength = Route.RouteLength,
                FromAddressId = (int)GetAddress(Route.FromAddress.Id),
                ToAddressId = (int)GetAddress(Route.ToAddress.Id),
                FromAddress = new()
                {
                    Id = GetAddress(Route.FromAddress.Id),
                    Country = Route.FromAddress.Country,
                    PostalCode = Route.FromAddress.PostalCode,
                    City = Route.FromAddress.City,
                    Street = Route.FromAddress.Street,
                    BuildingNumber = Route.FromAddress.BuildingNumber
                },
                ToAddress = new()
                {
                    Id = GetAddress(Route.ToAddress.Id),
                    Country = Route.ToAddress.Country,
                    PostalCode = Route.ToAddress.PostalCode,
                    City = Route.ToAddress.City,
                    Street = Route.ToAddress.Street,
                    BuildingNumber = Route.ToAddress.BuildingNumber
                },
            };

            return route;
        }

        private int? GetAddress(int? addressId)
        {
            if (addressId == 0)
            {
                return null;
            }

            return addressId;
        }

        private int? GetRouteId()
        {
            if (_isUpdateForm)
            {
                return Route.Id;
            }

            return null;
        }

        private void UpdateRoute(object obj)
        {
            Route route = CreateRoute();
            AddressRepository.Instance.UpdateAddress(route.FromAddress);
            AddressRepository.Instance.UpdateAddress(route.ToAddress);

            RouteRepository.Instance.UpdateRoute(route);
            SelectedViewModel.SelectedViewModel = new RoutesViewModel(SelectedViewModel);
        }
    }
}
