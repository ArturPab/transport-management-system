using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;
using transport_management_system.MVVM.Model;

namespace transport_management_system.Infrastructure.Domain
{
    public class CompanyRepository
    {
        private static CompanyRepository? _instance;
        public static CompanyRepository Instance => _instance ??= new CompanyRepository();
        private const string TableName = "Company";

        #region SELECT

        public IEnumerable<Company> GetAllCompanies()
        {
            var builder = new MySqlSelectQueryBuilder();
            var companys = builder.SelectAllProperties<Company>()
                .From(TableName)
                .Build()
                .ExecuteQuery<Company>();

            return companys;
        }

        public Company GetCompany(int id)
        {
            var builder = new MySqlSelectQueryBuilder();
            var company = builder.SelectAllProperties<Company>()
                .From(TableName)
                .Where("Id", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery<Company>().FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id: {id} doesn't exist");

            return company;
        }

        #endregion

        #region INSERT

        public int AddCompany(Company company)
        {
            var id = (int) new MySqlInsertQuery<Company>("Company", company).ExecuteQuery();

            company.Id = id;

            return id;
        }

        #endregion

        #region UPDATE

        public void UpdateCompany(Company company)
        {
            new MySqlUpdateQueryBuilder<Company>(company).Build().ExecuteQuery();
        }

        #endregion

        #region DELETE

        public void RemoveCompany(int id)
        {
            var builder = new MySqlSelectQueryBuilder();
            var orders = builder.SelectAllProperties<Order>()
                .From("Order")
                .Where("CompanyId", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery<Order>();

            if (orders.Any())
            {
                MessageBox.Show("Nie można usunąć firmy dla której wykonywane jest zlecenie");
                return;
            }

            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, id).Build().ExecuteQuery();
        }

        public void RemoveCompany(Company company)
        {
            if (company.Id == null)
                throw new ArgumentException("Company has no Id");

            var builder = new MySqlSelectQueryBuilder();
            var orders = builder.SelectAllProperties<Order>()
                .From("Order")
                .Where("CompanyId", WhereOperators.Equal, company.Id)
                .Build()
                .ExecuteQuery<Order>();

            if (orders.Any())
            {
                MessageBox.Show("Nie można usunąć firmy dla której wykonywane jest zlecenie");
                return;
            }

            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, company.Id).Build().ExecuteQuery();
        }

        #endregion
    }
}
