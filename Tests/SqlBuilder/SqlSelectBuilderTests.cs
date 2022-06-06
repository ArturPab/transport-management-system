using Xunit;
using FluentAssertions;
using transport_management_system.SQLBuilder;
using transport_management_system.SqlBuilder.Enums;

namespace Tests.SqlBuilder
{
    public class SqlSelectBuilderTests
    {
        [Fact]
        public void SqlSelectBuilderTests_ForGivenQueryParameters_BuildCorrectSQLQuery()
        {
            var builder = new SqlSelectQueryBuilder();

            var query1 = builder.Select("Name", "Age")
                .From("Person")
                .Where("Age", WhereOperators.Equal, 2)
                .Build();

            var query2 = builder.SelectAllProperties<Person>()
                .From("Person")
                .Where("Age", WhereOperators.Equal, 2)
                .Build();

            query1.Should().Be(query2);
            query2.Should().Be("SELECT Name, Age FROM Person WHERE Age = 2;");
        }

        private class Person
        {
            public string Name { get; set; }
            public uint Age { get; set; }
        }
    }
}
