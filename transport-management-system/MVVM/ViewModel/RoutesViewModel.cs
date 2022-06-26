using transport_management_system.Core;

namespace transport_management_system.MVVM.ViewModel
{
    internal class RoutesViewModel : ObservableObject
    {
        private NavigationViewModel selectedViewModel;

        public RoutesViewModel()
        {
        }

        public RoutesViewModel(NavigationViewModel selectedViewModel)
        {
            this.selectedViewModel = selectedViewModel;
        }
    }
}
