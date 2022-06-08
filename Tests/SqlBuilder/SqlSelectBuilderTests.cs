using Xunit;
using FluentAssertions;
using transport_management_system.SQLBuilder;
using transport_management_system.MVVM.Model;

namespace Tests.SqlBuilder
{
    public class SqlSelectBuilderTests
    {
        [Fact]
        public void SqlSelectBuilderTests_ForGivenQueryParameters_BuildCorrectSQLQuery()
        {
            var builder = new SqlSelectQueryBuilder();

            var companies = builder.SelectAllProperties<Company>()
                .From("company")
                .Build()
                .ExecuteQuery<Company>();


            companies.Should().HaveCount(1);
        }
    }
}
