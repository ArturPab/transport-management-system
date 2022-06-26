using System.Windows.Input;
using transport_management_system.Core;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.Domain;

namespace transport_management_system.MVVM.ViewModel
{
    internal class CompaniesFormViewModel
    {
        private bool _isUpdateForm = false;
        private NavigationViewModel SelectedViewModel { get; set; }
        public Company Company { get; set; }

        private ICommand _updateCompanyTableCommand;
        public ICommand UpdateCompanyTableCommand
        {
            get
            {
                if (_updateCompanyTableCommand == null)
                {
                    if (_isUpdateForm)
                    {
                        _updateCompanyTableCommand = new RelayCommand(UpdateCompany);
                    }
                    else
                    {
                        _updateCompanyTableCommand = new RelayCommand(AddCompany);
                    }
                }
                return _updateCompanyTableCommand;
            }
            set
            {
                _updateCompanyTableCommand = value;
            }
        }

        public CompaniesFormViewModel(NavigationViewModel selectedViewModel, object company)
        {
            SelectedViewModel = selectedViewModel;
            SetCompany(company);
        }

        private void SetCompany(object company)
        {
            if (IsCompany(company))
            {
                Company = (Company)company;
                _isUpdateForm = true;
            }
            else
            {
                Company = new Company();
                _isUpdateForm = false;
            }
        }

        private static bool IsCompany(object company)
        {
            return company != null && company is Company;
        }

        private void AddCompany(object obj)
        {
            CompanyRepository.Instance.AddCompany(Company);
            SelectedViewModel.SelectedViewModel = new CompaniesViewModel(SelectedViewModel);
        }

        private void UpdateCompany(object obj)
        {
            CompanyRepository.Instance.UpdateCompany(Company);
            SelectedViewModel.SelectedViewModel = new CompaniesViewModel(SelectedViewModel);
        }
    }
}
