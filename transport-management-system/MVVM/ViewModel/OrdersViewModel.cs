using transport_management_system.Core;

namespace transport_management_system.MVVM.ViewModel
{
    internal class OrdersViewModel : ObservableObject
    {
        private NavigationViewModel selectedViewModel;

        public OrdersViewModel()
        {
        }

        public OrdersViewModel(NavigationViewModel selectedViewModel)
        {
            this.selectedViewModel = selectedViewModel;
        }
    }
}
