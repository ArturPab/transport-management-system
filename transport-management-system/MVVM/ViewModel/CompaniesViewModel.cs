using System.Windows.Input;
using transport_management_system.Core;
using transport_management_system.MVVM.Model;

namespace transport_management_system.MVVM.ViewModel
{
    internal class CompaniesViewModel
    {
        private NavigationViewModel selectedViewModel;
        public CompaniesModel CompaniesModel { get; }
        private ICommand _deleteCompanyCommand;
        private ICommand _companyFormCommand;
        public ICommand DeleteCompanyCommand
        {
            get
            {
                if (_deleteCompanyCommand == null)
                {
                    _deleteCompanyCommand = new RelayCommand(CompaniesModel.DeleteCompany);
                }
                return _deleteCompanyCommand;
            }
            set
            {
                _deleteCompanyCommand = value;
            }
        }

        public ICommand CompanyFormCommand
        {
            get
            {
                if (_companyFormCommand == null)
                {
                    _companyFormCommand = new RelayCommand(RouteToCompanyForm);
                }
                return _companyFormCommand;
            }
            set
            {
                _companyFormCommand = value;
            }
        }

        public CompaniesViewModel(NavigationViewModel selectedViewModel)
        {
            this.selectedViewModel = selectedViewModel;
            CompaniesModel = new();
        }

        private void RouteToCompanyForm(object company)
        {
            selectedViewModel.SelectedViewModel = new CompaniesFormViewModel(selectedViewModel, company);
        }
    }
}
