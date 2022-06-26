using System.Windows.Input;
using transport_management_system.Core;

namespace transport_management_system.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public ICommand CarsViewCommand { get; set; }
        public ICommand CompaniesViewCommand { get; set; }
        public ICommand DriversViewCommand { get; set; }
        public ICommand OrdersViewCommand { get; set; }
        public ICommand RoutesViewCommand { get; set; }
        public NavigationViewModel SelectedViewModel { get; set; }

        public MainViewModel()
        {
            SelectedViewModel = new NavigationViewModel();
            SelectedViewModel.SelectedViewModel = new OrdersViewModel(SelectedViewModel);

            CarsViewCommand = new RelayCommand(o =>
            {
                SelectedViewModel.SelectedViewModel = new CarsViewModel(SelectedViewModel);
            });

            CompaniesViewCommand = new RelayCommand(o =>
            {
                SelectedViewModel.SelectedViewModel = new CompaniesViewModel(SelectedViewModel);
            });

            DriversViewCommand = new RelayCommand(o =>
            {
                SelectedViewModel.SelectedViewModel = new DriversViewModel(SelectedViewModel);
            });

            OrdersViewCommand = new RelayCommand(o =>
            {
                SelectedViewModel.SelectedViewModel = new OrdersViewModel(SelectedViewModel);
            });

            RoutesViewCommand = new RelayCommand(o =>
            {
                SelectedViewModel.SelectedViewModel = new RoutesViewModel(SelectedViewModel);
            });
        }
    }
}
