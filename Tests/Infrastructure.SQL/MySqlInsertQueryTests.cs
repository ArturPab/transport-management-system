using FluentAssertions;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;
using Xunit;

namespace Tests.Infrastructure.SQL
{
    public class MySqlInsertQueryTests
    {
        [Fact]
        public void SqlInsertQuery_ForGivenModel_AddToDatabase()
        {
            var car = new Car("Skoda", "Fabia", 2000, "2000", "123456");

            var id = new MySqlInsertQuery<Car>("car", car).ExecuteQuery();


            var selectBuilder = new MySqlSelectQueryBuilder();
            var selectedCars = selectBuilder.SelectAllProperties<Car>()
                .From("car")
                .Where("Id", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery<Car>();

            Assert.NotNull(id);
            selectedCars.Should().HaveCount(1);
        }
    }
}
