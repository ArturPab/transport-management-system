using System.Collections.Generic;
using System.Windows.Input;
using transport_management_system.Common.Lookups;
using transport_management_system.Core;
using transport_management_system.Domain;
using transport_management_system.Domain.DTO;
using transport_management_system.Infrastructure.Domain;

namespace transport_management_system.MVVM.ViewModel
{
    internal class CarsFormViewModel
    {
        private Dictionary<int, string> _carStatuses;
        public Dictionary<int, string> CarStatuses => _carStatuses;

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
            SetCar(car);
            SetCurrentStatusId();
            SetCarStatuses();
            SelectedViewModel = selectedViewModel;
        }

        private void SetCarStatuses()
        {
            _carStatuses = CarStatusLookup.Descriptions;
        }

        private void SetCurrentStatusId()
        {
            if(Car.StatusId == 0)
            {
                CurrentStatusId = Car.StatusId;
            } else
            {
                CurrentStatusId = Car.StatusId - 1;
            }
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
                StatusId = CurrentStatusId+1
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
