using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using transport_management_system.Core;
using transport_management_system.Domain;
using transport_management_system.Domain.DTO;
using transport_management_system.Infrastructure.Domain;

namespace transport_management_system.MVVM.Model
{
    internal class CarsModel : ObservableObject
    {
        private ObservableCollection<CarDTO> _cars;
        public ObservableCollection<CarDTO> Cars
        {
            get { return _cars; }
            set
            {
                _cars = value;
                OnPropertyChanged("Cars");
            }
        }

        public CarsModel()
        {
            SetCars();
        }

        private void SetCars()
        {
            IEnumerable<Car> cars = CarRepository.Instance.GetAllCars();
            ConvertCarsToCarDTOs(cars);
        }

        private void ConvertCarsToCarDTOs(IEnumerable<Car> fetchedCars)
        {
            Cars = new ObservableCollection<CarDTO>();
            if (fetchedCars.Any())
            {
                foreach (Car fetchedCar in fetchedCars)
                {
                    Cars.Add(new CarDTO(fetchedCar));
                }
            }
        }

        internal void DeleteCar(object car)
        {
            if (IsCar(car))
            {
                int carId = (int)((CarDTO) car).Id;
                CarRepository.Instance.RemoveCar(carId);
                SetCars();
            }
        }

        private static bool IsCar(object car)
        {
            return car != null && car is CarDTO;
        }
    }
}
