using System;
using System.Windows.Input;
using transport_management_system.Core;
using transport_management_system.MVVM.Model;

namespace transport_management_system.MVVM.ViewModel
{
    internal class DriversViewModel
    {
        private NavigationViewModel selectedViewModel;
        public DriversModel DriversModel{ get; }
        private ICommand _deleteDriverCommand;
        private ICommand _driverFormCommand;
        public ICommand DeleteDriverCommand
        {
            get
            {
                if (_deleteDriverCommand == null)
                {
                    _deleteDriverCommand = new RelayCommand(DriversModel.DeleteDriver);
                }
                return _deleteDriverCommand;
            }
            set
            {
                _deleteDriverCommand = value;
            }
        }

        public ICommand DriverFormCommand
        {
            get
            {
                if (_driverFormCommand == null)
                {
                    _driverFormCommand = new RelayCommand(RouteToDriverForm);
                }
                return _driverFormCommand;
            }
            set
            {
                _driverFormCommand = value;
            }
        }

        public DriversViewModel(NavigationViewModel selectedViewModel)
        {
            this.selectedViewModel = selectedViewModel;
            DriversModel = new();
        }

        private void RouteToDriverForm(object driver)
        {
            selectedViewModel.SelectedViewModel = new DriversFormViewModel(selectedViewModel, driver);
        }
    }
}
