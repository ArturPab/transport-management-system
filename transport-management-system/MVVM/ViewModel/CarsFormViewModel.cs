using System.Collections.ObjectModel;
using System.Windows.Input;
using transport_management_system.Common.Lookups;
using transport_management_system.Core;
using transport_management_system.Domain;
using transport_management_system.Domain.DTO;
using transport_management_system.Infrastructure.Domain;
using transport_management_system.MVVM.Model;

namespace transport_management_system.MVVM.ViewModel
{
    internal class CarsFormViewModel
    {
        public Collection<CarStatusModel> CarStatuses;
        private bool _isUpdateForm = false;
        private NavigationViewModel SelectedViewModel { get; set; }
        public CarDTO Car { get; set; }
        public int CurrentStatusId { get; set; }

        private ICommand _updateCarTableCommand;
        public ICommand UpdateCarTableCommand
        {
            get
            {
                if (_updateCarTableCommand == null)
                {
                    if (_isUpdateForm)
                    {
                        _updateCarTableCommand = new RelayCommand(UpdateCar);
                    }
                    else
                    {
                        _updateCarTableCommand = new RelayCommand(AddCar);
                    }
                }
                return _updateCarTableCommand;
            }
            set
            {
                _updateCarTableCommand = value;
            }
        }

        public CarsFormViewModel(NavigationViewModel selectedViewModel, object car)
        {
            SetCarStatuses();
            SetCar(car);
            SetCurrentStatusId();
            SelectedViewModel = selectedViewModel;
        }

        private void SetCarStatuses()
        {
            CarStatuses = new Collection<CarStatusModel>();
            foreach (var carStatusDescription in CarStatusLookup.Descriptions) {
                CarStatuses.Add(new()
                {
                    StatusId = carStatusDescription.Key,
                    Status = carStatusDescription.Value
                });
            }
        }

        private void SetCurrentStatusId()
        {
            CurrentStatusId = Car.StatusId;
        }

        private void SetCar(object car)
        {
            if (IsCar(car))
            {
                Car = (CarDTO)car;
                _isUpdateForm = true;
            }
            else
            {
                Car = new CarDTO();
                _isUpdateForm = false;
            }
        }

        private static bool IsCar(object car)
        {
            return car != null && car is CarDTO;
        }

        private void AddCar(object obj)
        {
            Car car = CreateCar();
            CarRepository.Instance.AddCar(car);
            SelectedViewModel.SelectedViewModel = new CarsViewModel(SelectedViewModel);
        }

        private Car CreateCar()
        {
            Car car = new()
            {
                Id = GetCarId(),
                Mark = Car.Mark,
                Model = Car.Model,
                Payload = Car.Payload,
                ProductionYear = Car.ProductionYear,
                Vin = Car.Vin,
                StatusId = GetStatusId()
        };

            return car;
        }

        private int? GetCarId()
        {
            if (_isUpdateForm)
            {
                return Car.Id;
            }

            return null;
        }

        private int GetStatusId()
        {
            if (_isUpdateForm)
            {
                foreach (var status in CarStatusLookup.Descriptions)
                {
                    if (status.Value.Equals(Car.Status))
                    {
                        return status.Key;
                    }
                }
            }

            return 1;
        }

        private void UpdateCar(object obj)
        {
            Car car = CreateCar();
            CarRepository.Instance.UpdateCar(car);
            SelectedViewModel.SelectedViewModel = new CarsViewModel(SelectedViewModel);
        }
    }
}
