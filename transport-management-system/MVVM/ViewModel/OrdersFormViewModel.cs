using System;
using System.Collections.Generic;
using System.Windows.Input;
using transport_management_system.Common.Lookups;
using transport_management_system.Core;
using transport_management_system.Domain;
using transport_management_system.Domain.DTO;
using transport_management_system.Infrastructure.Domain;
using transport_management_system.MVVM.Model;

namespace transport_management_system.MVVM.ViewModel
{
    internal class OrdersFormViewModel
    {
        private Dictionary<int, string> _orderStatuses;
        public Dictionary<int, string> OrderStatuses => _orderStatuses;
        public List<RouteDTO> RouteDTOs { get; set; }
        public List<Company> Companies { get; set; }

        private bool _isUpdateForm = false;
        private NavigationViewModel SelectedViewModel { get; set; }
        public OrderDTO Order { get; set; }
        public int CurrentStatusId { get; set; }
        public RouteDTO CurrentRoute { get; set; }
        public Company CurrentCompany { get; set; }

        private ICommand _updateOrderTableCommand;
        public ICommand UpdateOrderTableCommand
        {
            get
            {
                if (_updateOrderTableCommand == null)
                {
                    if (_isUpdateForm)
                    {
                        _updateOrderTableCommand = new RelayCommand(UpdateOrder);
                    }
                    else
                    {
                        _updateOrderTableCommand = new RelayCommand(AddOrder);
                    }
                }
                return _updateOrderTableCommand;
            }
            set
            {
                _updateOrderTableCommand = value;
            }
        }

        public OrdersFormViewModel(NavigationViewModel selectedViewModel, object order)
        {
            SetOrder(order);
            SetCurrentRoute();
            SetCurrentCompany();
            SetCurrentStatusId();
            SetOrderStatuses();
            SetRouteDTOs();
            SetCompanies();
            SelectedViewModel = selectedViewModel;
        }

        private void SetCompanies()
        {
            IEnumerable<Company> fetchedCompanies = CompanyRepository.Instance.GetAllCompanies();
            Companies = new List<Company>();
            foreach (Company fetchCompany in fetchedCompanies)
            {
                Companies.Add(fetchCompany);
            }
        }

        private void SetRouteDTOs()
        {
            IEnumerable<Route> fetchedRoutes = RouteRepository.Instance.GetAllRoutes();
            RouteDTOs = new List<RouteDTO>();
            foreach (Route fetchRoute in fetchedRoutes)
            {
                RouteDTOs.Add(new RouteDTO(fetchRoute));
            }
        }

        private void SetOrderStatuses()
        {
            _orderStatuses = OrderStatusLookup.Descriptions;
        }

        private void SetCurrentStatusId()
        {
            if (_isUpdateForm)
            {
                CurrentStatusId = Order.OrderStatusId - 1;
            }
        }

        private void SetCurrentCompany()
        {
            if (_isUpdateForm)
            {
                CurrentCompany = Order.Company;
            }
        }

        private void SetCurrentRoute()
        {
            if (_isUpdateForm)
            {
                CurrentRoute = Order.RouteDTO;
            }
        }

        private void SetOrder(object order)
        {
            if (IsOrder(order))
            {
                Order = (OrderDTO)order;
                _isUpdateForm = true;
            }
            else
            {
                Order = new OrderDTO();
                _isUpdateForm = false;
            }
        }

        private static bool IsOrder(object order)
        {
            return order != null && order is OrderDTO;
        }

        private void AddOrder(object obj)
        {
            Order order = CreateOrder();
            OrderRepository.Instance.AddOrder(order);
            SelectedViewModel.SelectedViewModel = new OrdersViewModel(SelectedViewModel);
        }

        private Order CreateOrder()
        {
            Order order = new()
            {
                Id = GetOrderId(),
                RouteId = CurrentRoute.Id,
                Created = GetCreatedDate(),
                NumberOfCourses = Order.NumberOfCourses,
                Price = Order.Price,
                CompanyId = CurrentCompany.Id,
                StatusId = CurrentStatusId+1,
            };

            return order;
        }

        private DateTime GetCreatedDate()
        {
            return Order.Created;
        }

        private int? GetOrderId()
        {
            if (_isUpdateForm)
            {
                return Order.Id;
            }

            return null;
        }

        private void UpdateOrder(object obj)
        {
            Order order = CreateOrder();
            OrderRepository.Instance.UpdateOrder(order);
            SelectedViewModel.SelectedViewModel = new OrdersViewModel(SelectedViewModel);
        }
    }
}
