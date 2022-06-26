using transport_management_system.Core;

namespace transport_management_system.MVVM.ViewModel
{
    internal class CarsViewModel : ObservableObject
    {
        private NavigationViewModel selectedViewModel;
        public CarsViewModel()
        {
        }

        public CarsViewModel(NavigationViewModel selectedViewModel)
        {
            this.selectedViewModel = selectedViewModel;
        }
    }
}
