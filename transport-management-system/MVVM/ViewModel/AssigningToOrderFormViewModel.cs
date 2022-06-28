using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using transport_management_system.Core;
using transport_management_system.Domain;
using transport_management_system.Domain.DTO;
using transport_management_system.Infrastructure.Domain;
using transport_management_system.MVVM.Model;

namespace transport_management_system.MVVM.ViewModel
{
    internal class AssigningToOrderFormViewModel
    {
        private NavigationViewModel SelectedViewModel { get; set; }
        public OrderDTO Order { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<int> CurrentDriverIds { get; set; }
        public List<Car> Cars { get; set; }
        public List<int> CurrentCarIds { get; set; }
        private ICommand _assignDriversAndCarsToOrderTableCommand;
        public ICommand AssignDriversAndCarsToOrderTableCommand
        {
            get
            {
                if (_assignDriversAndCarsToOrderTableCommand == null)
                {
                    _assignDriversAndCarsToOrderTableCommand = new RelayCommand(AssignDriversAndCarsToOrder);
                }
                return _assignDriversAndCarsToOrderTableCommand;
            }
            set
            {
                _assignDriversAndCarsToOrderTableCommand = value;
            }
        }

        public AssigningToOrderFormViewModel(NavigationViewModel selectedViewModel, object order)
        {
            SetOrder(order);
            SetDrivers();
            SetCurrentDrivers();
            SetCars();
            SetCurrentCars();
            SelectedViewModel = selectedViewModel;
        }

        private void SetCurrentCars()
        {
            List<Car> currentCars = OrderRepository.Instance.GetAssignCarsForOrder((int)Order.Id);
            foreach (Car currentCar in currentCars)
            {
                Cars.Where(car => car.Id == currentCar.Id).ToList().ForEach(car => car.IsSelected = true);
            }
        }

        private void SetCars()
        {
            Cars = (List<Car>)CarRepository.Instance.GetAllCars();
        }

        private void SetCurrentDrivers()
        {
            List<Driver> currentDrivers = OrderRepository.Instance.GetAssignDriversForOrder((int)Order.Id);
            foreach (Driver currentDriver in currentDrivers)
            {
                Drivers.Where(driver => driver.Id == currentDriver.Id).ToList().ForEach(driver => driver.IsSelected = true);
            }
        }

        private void SetDrivers()
        {
            Drivers = (List<Driver>)DriverRepository.Instance.GetAllDrivers();
        }

        private void SetOrder(object order)
        {
                Order = (OrderDTO)order;
        }

        private void AssignDriversAndCarsToOrder(object obj)
        {
            AssignDriversToOrder();
            AssignCarsToOrder();
            SelectedViewModel.SelectedViewModel = new OrdersViewModel(SelectedViewModel);
        }

        private void AssignDriversToOrder()
        {
            CurrentDriverIds = Drivers.Where(driver => driver.IsSelected).Select(driver => (int)driver.Id).ToList();
            OrderRepository.Instance.AssignDriversToOrder((int)Order.Id, CurrentDriverIds);
        }

        private void AssignCarsToOrder()
        {
            CurrentCarIds = Cars.Where(car => car.IsSelected).Select(car => (int)car.Id).ToList();
            OrderRepository.Instance.AssignCarsToOrder((int)Order.Id, CurrentCarIds);
        }
    }
}
