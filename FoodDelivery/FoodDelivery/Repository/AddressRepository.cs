using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Contracts;
using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;
using System.Data;

namespace FoodDelivery.Repository
{
    public class AddressRepository:IAddressRepository
    {
        private readonly DapperContext _context;

        public AddressRepository(DapperContext context) => _context = context;

        public async Task<Addresses> GetAddresses(int id)
        {
            var query = "SELECT address_id as address_id,houseNumber as HouseNumber,streetName as StreetName,city as City,postalCode as PostalCode from " +
                "Addresses where address_id=@id";

            using (var connection = _context.CreateConnection())
            {
                var address = await connection.QuerySingleAsync<Addresses>(query, new { id });
                return address;
            }
        }

        public async Task UpdateAddress(int id, AddressForUpdateDto address)
        {
            var query = "UPDATE Addresses SET houseNumber = @houseNumber,streetName = @streetName,city = @city," +
                "postalCode = @postalCode where address_id=@id";
            
            var parameters = new DynamicParameters();

            parameters.Add("id", id, DbType.Int32);
            parameters.Add("houseNumber",address.HouseNumber, DbType.Int32);
            parameters.Add("streetName",address.StreetName, DbType.String);
            parameters.Add("city",address.City, DbType.String);
            parameters.Add("postalCode",address.PostalCode, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

            

        }
    }
}   
