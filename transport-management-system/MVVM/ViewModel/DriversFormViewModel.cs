using System.ComponentModel;
using System.Windows.Input;
using transport_management_system.Core;
using transport_management_system.Infrastructure.Domain;
using transport_management_system.MVVM.Model;

namespace transport_management_system.MVVM.ViewModel
{
    internal class DriversFormViewModel
    {
        private bool _isUpdateForm = false;
        private NavigationViewModel SelectedViewModel { get; set; }
        public Driver Driver { get; set; }

        private ICommand _updateDriverTableCommand;
        public ICommand UpdateDriverTableCommand
        {
            get
            {
                if (_updateDriverTableCommand == null)
                {
                    if (_isUpdateForm)
                    {
                        _updateDriverTableCommand = new RelayCommand(UpdateDriver);
                    }
                    else
                    {
                        _updateDriverTableCommand = new RelayCommand(AddDriver);
                    }
                }
                return _updateDriverTableCommand;
            }
            set
            {
                _updateDriverTableCommand = value;
            }
        }

        public DriversFormViewModel(NavigationViewModel selectedViewModel, object driver)
        {
            SelectedViewModel = selectedViewModel;
            SetDriver(driver);
        }

        private void SetDriver(object driver)
        {
            if (IsDriver(driver))
            {
                Driver = (Driver)driver;
                _isUpdateForm = true;
            }
            else
            {
                Driver = new Driver();
                _isUpdateForm = false;
            }
        }

        private static bool IsDriver(object driver)
        {
            return driver != null && driver is Driver;
        }

        private void AddDriver(object obj)
        {
            DriverRepository.Instance.AddDriver(Driver);
            SelectedViewModel.SelectedViewModel = new DriversViewModel(SelectedViewModel);
        }

        private void UpdateDriver(object obj)
        {
            DriverRepository.Instance.UpdateDriver(Driver);
            SelectedViewModel.SelectedViewModel = new DriversViewModel(SelectedViewModel);
        }
    }
}
