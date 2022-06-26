using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using transport_management_system.Core;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.Domain;

namespace transport_management_system.MVVM.Model
{
    internal class CompaniesModel : ObservableObject
    {
        private ObservableCollection<Company> _companies;
        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set
            {
                _companies = value;
                OnPropertyChanged("Companies");
            }
        }

        public CompaniesModel()
        {
            SetCompanies();
        }

        private void SetCompanies()
        {
            IEnumerable<Company> companies = CompanyRepository.Instance.GetAllCompanies();
            ConvertCompaniesToObservableCollection(companies);
        }

        private void ConvertCompaniesToObservableCollection(IEnumerable<Company> fetchedCompanies)
        {
            Companies = new ObservableCollection<Company>();
            if (fetchedCompanies.Any())
            {
                foreach (Company fetchedCompany in fetchedCompanies)
                {
                    Companies.Add(fetchedCompany);
                }
            }
        }

        internal void DeleteCompany(object company)
        {
            if (IsCompany(company))
            {
                CompanyRepository.Instance.RemoveCompany((Company)company);
                SetCompanies();
            }
        }

        private static bool IsCompany(object company)
        {
            return company != null && company is Company;
        }
    }
}
