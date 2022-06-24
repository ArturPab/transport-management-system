using System.Linq;
using FluentAssertions;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;
using Xunit;

namespace Tests.Infrastructure.SQL
{
    public class MySqlUpdateQueryTests
    {
        [Fact]
        public void SqlInsertQuery_ForGivenModel_AddToDatabase()
        {
            var car =  new Car("Skoda", "Fabia", 2000, "2000", "123456");

            car.Id = (int) new MySqlInsertQuery<Car>("car", car).ExecuteQuery();

            car.Mark = "Nissan";
            new MySqlUpdateQueryBuilder<Car>(car).Build().ExecuteQuery();

            var selectBuilder = new MySqlSelectQueryBuilder();
            var selectedCars = selectBuilder.SelectAllProperties<Car>()
                .From("car")
                .Where("Id", WhereOperators.Equal, car.Id)
                .Build()
                .ExecuteQuery<Car>();

            selectedCars.First().Mark.Should().Be("Nissan");
        }
    }
}
