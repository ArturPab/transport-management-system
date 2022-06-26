using System;
using System.Collections.Generic;
using System.Linq;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;

namespace transport_management_system.Infrastructure.Domain
{
    public class CarRepository
    {
        private static CarRepository? _instance;
        public static CarRepository Instance => _instance ??= new CarRepository();

        private const string TableName = "Car";

        #region SELECT

        public IEnumerable<Car> GetAllCars()
        {
            var builder = new MySqlSelectQueryBuilder();
            var cars = builder.SelectAllProperties<Car>()
                .From(TableName)
                .Build()
                .ExecuteQuery<Car>();

            return cars;
        }

        public Car GetCar(int id)
        {
            var builder = new MySqlSelectQueryBuilder();
            var car = builder.SelectAllProperties<Car>()
                .From(TableName)
                .Where("Id", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery<Car>().FirstOrDefault();

            if (car == null)
                throw new ArgumentException($"Car with id: {id} doesn't exist");

            return car;
        }

        #endregion

        #region INSERT

        public int AddCar(Car car)
        {
            var id = (int) new MySqlInsertQuery<Car>("Car", car).ExecuteQuery();

            car.Id = id;

            return id;
        }

        #endregion

        #region UPDATE

        public void UpdateCar(Car car)
        {
            new MySqlUpdateQueryBuilder<Car>(car).Build().ExecuteQuery();
        }

        #endregion

        #region DELETE

        public void RemoveCar(int id)
        {
            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, id).Build().ExecuteQuery();

        }

        public void RemoveCar(Car car)
        {
            if (car.Id == null)
                throw new ArgumentException("Car has no Id");

            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, car.Id).Build().ExecuteQuery();
        }

        #endregion
    }
}
