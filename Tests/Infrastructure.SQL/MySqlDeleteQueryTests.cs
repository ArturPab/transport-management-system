using FluentAssertions;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;
using transport_management_system.MVVM.Model;
using Xunit;

namespace Tests.Infrastructure.SQL
{
    public class MySqlDeleteQueryTests
    {
        [Fact]
        public void MySqlDeleteQuery_ForGivenId_DeleteARightDatabaseRecord()
        {
            var driver = new Driver
            {
                Id = null,
                FirstName = "Adam",
                LastName = "Szklany",
                Pesel = "12345678901",
                Salary = 3600
            };
            var id = new MySqlInsertQuery<Driver>("driver", driver).ExecuteQuery();

            new MySqlDeleteQueryBuilder()
                .From("driver")
                .Where("Id", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery();

            var selectedDriver = new MySqlSelectQueryBuilder()
                .SelectAllProperties<Driver>()
                .From("driver")
                .Where("Id", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery<Driver>();

            selectedDriver.Should().BeEmpty();
        }
    }
}
