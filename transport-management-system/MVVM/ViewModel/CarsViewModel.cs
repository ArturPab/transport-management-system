using System.Windows.Input;
using transport_management_system.Core;
using transport_management_system.MVVM.Model;

namespace transport_management_system.MVVM.ViewModel
{
    internal class CarsViewModel : ObservableObject
    {
        private NavigationViewModel selectedViewModel;
        public CarsModel CarsModel { get; }
        private ICommand _deleteCarCommand;
        private ICommand _carFormCommand;
        public ICommand DeleteCarCommand
        {
            get
            {
                if (_deleteCarCommand == null)
                {
                    _deleteCarCommand = new RelayCommand(CarsModel.DeleteCar);
                }
                return _deleteCarCommand;
            }
            set
            {
                _deleteCarCommand = value;
            }
        }

        public ICommand CarFormCommand
        {
            get
            {
                if (_carFormCommand == null)
                {
                    _carFormCommand = new RelayCommand(RouteToCarsForm);
                }
                return _carFormCommand;
            }
            set
            {
                _carFormCommand = value;
            }
        }

        public CarsViewModel(NavigationViewModel selectedViewModel)
        {
            this.selectedViewModel = selectedViewModel;
            CarsModel = new();
        }

        private void RouteToCarsForm(object car)
        {
            selectedViewModel.SelectedViewModel = new CarsFormViewModel(selectedViewModel, car);
        }
    }
}
