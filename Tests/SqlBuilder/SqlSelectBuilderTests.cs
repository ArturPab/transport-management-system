using Xunit;
using FluentAssertions;
using transport_management_system.Database.SQL.QueryBuilders;
using transport_management_system.Domain;

namespace Tests.SqlBuilder
{
    public class SqlSelectBuilderTests
    {
        [Fact]
        public void SqlSelectBuilderTests_ForGivenQueryParameters_BuildCorrectSQLQuery()
        {
            var builder = new MySqlSelectQueryBuilder();

            var companies = builder.SelectAllProperties<Company>()
                .From("company")
                .Build()
                .ExecuteQuery<Company>();

            companies.Should().HaveCount(1);
        }
    }
}
