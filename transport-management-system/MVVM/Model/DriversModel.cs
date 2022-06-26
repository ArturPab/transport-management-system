using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using transport_management_system.Core;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.Domain;

namespace transport_management_system.MVVM.Model
{
    internal class DriversModel : ObservableObject
    {
        private ObservableCollection<Driver> _drivers;
        public ObservableCollection<Driver> Drivers { 
            get { return _drivers; }
            set {
                _drivers = value;
                OnPropertyChanged("Drivers");
            }
        }

        public DriversModel()
        {
            SetDrivers();
        }

        private void SetDrivers()
        {
            IEnumerable<Driver> drivers = DriverRepository.Instance.GetAllDrivers();
            ConvertDriversToObservableCollection(drivers);
        }

        private void ConvertDriversToObservableCollection(IEnumerable<Driver> fetchedDrivers)
        {
            Drivers = new ObservableCollection<Driver>();
            if (fetchedDrivers.Any())
            {
                foreach (Driver fetchedDriver in fetchedDrivers)
                {
                    Drivers.Add(fetchedDriver);
                }
            }
        }

        internal void DeleteDriver(object driver)
        {
            if (IsDriver(driver))
            {
                DriverRepository.Instance.RemoveDriver((Driver)driver);
                SetDrivers();
            }
        }

        private static bool IsDriver(object driver)
        {
            return driver != null && driver is Driver;
        }
    }
}
