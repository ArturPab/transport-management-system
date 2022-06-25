using System;
using System.Collections.Generic;
using System.Linq;
using transport_management_system.Domain;
using transport_management_system.Infrastructure.SQL;
using transport_management_system.Infrastructure.SQL.Enums;
using transport_management_system.Infrastructure.SQL.QueryBuilders;

namespace transport_management_system.Infrastructure.Domain
{
    public class AddressRepository
    {
        private const string TableName = "Address";

        #region SELECT

        public IEnumerable<Address> GetAllAddress()
        {
            var builder = new MySqlSelectQueryBuilder();
            var addresses = builder.SelectAllProperties<Address>()
                .From(TableName)
                .Build()
                .ExecuteQuery<Address>();

            return addresses;
        }

        public Address GetAddress(int id)
        {
            var builder = new MySqlSelectQueryBuilder();
            var address = builder.SelectAllProperties<Address>()
                .From(TableName)
                .Where("Id", WhereOperators.Equal, id)
                .Build()
                .ExecuteQuery<Address>().FirstOrDefault();

            if (address == null)
                throw new ArgumentException($"Address with id: {id} doesn't exist");

            return address;
        }

        #endregion

        #region INSERT

        public int AddAddress(Address address)
        {
            var id = (int) new MySqlInsertQuery<Address>("Address", address).ExecuteQuery();

            address.Id = id;

            return id;
        }

        #endregion

        #region UPDATE

        public void UpdateAddress(Address Address)
        {
            new MySqlUpdateQueryBuilder<Address>(Address).Build().ExecuteQuery();
        }

        #endregion

        #region DELETE

        public void RemoveAddress(int id)
        {
            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, id).Build().ExecuteQuery();

        }

        public void RemoveAddress(Address address)
        {
            if (address.Id == null)
                throw new ArgumentException("Address has no Id");

            new MySqlDeleteQueryBuilder().From(TableName).Where("Id", WhereOperators.Equal, address.Id).Build().ExecuteQuery();
        }

        #endregion
    }
}
