using System;
using System.Collections.Generic;
using System.Linq;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;
using transport_management_system.MVVM.Model;

namespace transport_management_system.Infrastructure.Domain
{
    public class DriverRepository
    {
        private static DriverRepository? _instance;
        public static DriverRepository Instance => _instance ??= new DriverRepository();

        private const string TableName = "Driver";

        #region SELECT

        public IEnumerable<Driver> GetAllDrivers()
        {
            var builder = new MySqlSelectQueryBuilder();
            var drivers = builder.SelectAllProperties<Driver>()
                .From(TableName)
                .Build()
                .ExecuteQuery<Driver>();

            return drivers;
        }

        public Driver GetDriver(int id)
        {
            var builder = new MySqlSelectQueryBuilder();
            var driver = builder.SelectAllProperties<Driver>()
                .From(TableName)
                .Where("Id", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery<Driver>().FirstOrDefault();

            if (driver == null)
                throw new ArgumentException($"Driver with id: {id} doesn't exist");

            return driver;
        }

        #endregion

        #region INSERT

        public int AddDriver(Driver driver)
        {
            var id = (int) new MySqlInsertQuery<Driver>("Driver", driver).ExecuteQuery();

            driver.Id = id;

            return id;
        }

        #endregion

        #region UPDATE

        public void UpdateDriver(Driver driver)
        {
            new MySqlUpdateQueryBuilder<Driver>(driver).Build().ExecuteQuery();
        }

        #endregion

        #region DELETE

        public void RemoveDriver(int id)
        {
            new MySqlDeleteQueryBuilder().From("performs").Where("DriverId", WhereOperators.Equal, id).Build().ExecuteQuery();
            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, id).Build().ExecuteQuery();

        }

        public void RemoveDriver(Driver driver)
        {
            if (driver.Id == null)
                throw new ArgumentException("Driver has no Id");
            new MySqlDeleteQueryBuilder().From("performs").Where("DriverId", WhereOperators.Equal, driver.Id).Build().ExecuteQuery();
            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, driver.Id).Build().ExecuteQuery();
        }

        #endregion
    }
}
