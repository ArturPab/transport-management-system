using transport_management_system.Core;

namespace transport_management_system.MVVM.ViewModel
{
    internal class CompaniesViewModel : ObservableObject
    {
        private NavigationViewModel selectedViewModel;
        public CompaniesViewModel()
        {
        }

        public CompaniesViewModel(NavigationViewModel selectedViewModel)
        {
            this.selectedViewModel = selectedViewModel;
        }
    }
}
