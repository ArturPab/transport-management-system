using System;
using System.Collections.Generic;
using System.Linq;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;

namespace transport_management_system.Infrastructure.Domain
{
    public class CompanyRepository
    {
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
            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, id);

        }

        public void RemoveCompany(Company company)
        {
            if (company.Id == null)
                throw new ArgumentException("Company has no Id");

            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, company.Id);
        }

        #endregion
    }
}
