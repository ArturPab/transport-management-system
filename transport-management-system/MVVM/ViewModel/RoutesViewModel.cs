using System.Windows.Input;
using transport_management_system.Core;
using transport_management_system.MVVM.Model;

namespace transport_management_system.MVVM.ViewModel
{
    internal class RoutesViewModel : ObservableObject
    {
        private NavigationViewModel selectedViewModel;
        public RoutesModel RoutesModel { get; }
        private ICommand _deleteRouteCommand;
        private ICommand _routeFormCommand;
        public ICommand DeleteRouteCommand
        {
            get
            {
                if (_deleteRouteCommand == null)
                {
                    _deleteRouteCommand = new RelayCommand(RoutesModel.DeleteRoute);
                }
                return _deleteRouteCommand;
            }
            set
            {
                _deleteRouteCommand = value;
            }
        }

        public ICommand RouteFormCommand
        {
            get
            {
                if (_routeFormCommand == null)
                {
                    _routeFormCommand = new RelayCommand(RouteToRoutesForm);
                }
                return _routeFormCommand;
            }
            set
            {
                _routeFormCommand = value;
            }
        }

        public RoutesViewModel(NavigationViewModel selectedViewModel)
        {
            this.selectedViewModel = selectedViewModel;
            RoutesModel = new();
        }

        private void RouteToRoutesForm(object route)
        {
            selectedViewModel.SelectedViewModel = new RoutesFormViewModel(selectedViewModel, route);
        }
    }
}
