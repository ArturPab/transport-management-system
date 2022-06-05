using transport_management_system.Core;

namespace transport_management_system.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand CarsViewCommand { get; set; }
        public RelayCommand CompaniesViewCommand { get; set; }
        public RelayCommand DriversViewCommand { get; set; }
        public RelayCommand OrdersViewCommand { get; set; }
        public RelayCommand RoutesViewCommand { get; set; }

        public CarsViewModel CarsVM { get; set; }
        public CompaniesViewModel CompaniesVM { get; set; }
        public DriversViewModel DriversVM { get; set; }
        public OrdersViewModel OrdersVM { get; set; }
        public RoutesViewModel RoutesVM { get; set; }

        private object currentView;

        public object CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            CarsVM = new CarsViewModel();
            CompaniesVM = new CompaniesViewModel();
            DriversVM = new DriversViewModel();
            OrdersVM = new OrdersViewModel();
            RoutesVM = new RoutesViewModel();

            CurrentView = OrdersVM;

            CarsViewCommand = new RelayCommand(o =>
            {
                CurrentView = CarsVM;
            });

            CompaniesViewCommand = new RelayCommand(o =>
            {
                CurrentView = CompaniesVM;
            });

            DriversViewCommand = new RelayCommand(o =>
            {
                CurrentView = DriversVM;
            });

            OrdersViewCommand = new RelayCommand(o =>
            {
                CurrentView = OrdersVM;
            });

            RoutesViewCommand = new RelayCommand(o =>
            {
                CurrentView = RoutesVM;
            });
        }
    }
}
