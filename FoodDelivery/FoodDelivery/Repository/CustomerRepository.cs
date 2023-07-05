using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Contracts;
using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;
using System.Data;

namespace FoodDelivery.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly DapperContext _context;
        public CustomerRepository(DapperContext context) => _context = context;

        public async Task<Customer> AddCustomer(CustomerForAddDto customer)
        {
            var query = "INSERT INTO Customers(firstName,lastName)VALUES(@first_name,@last_name)"+
                "SELECT CAST(SCOPE_IDENTITY() AS int)"; ;

            var parameters = new DynamicParameters();
            parameters.Add("first_name", customer.firstName, DbType.String);
            parameters.Add("last_name", customer.lastName, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query,parameters);
                var addedCustomer = new Customer
                {
                    customer_id = id,
                    first_name = customer.firstName,
                    last_name = customer.lastName,
                };
                return addedCustomer;

            }
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var query = "SELECT customer_id,firstName as first_name,lastName as last_name FROM Customers where customer_id=@id";

            using (var connection = _context.CreateConnection())
            {
                var customer = await connection.QuerySingleOrDefaultAsync<Customer>(query, new { id });
                return customer;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var query = "SELECT customer_id,firstName as first_name,lastName as last_name FROM Customers";
            using (var connection = _context.CreateConnection())
            {
                var customers = await connection.QueryAsync<Customer>(query);

                return customers.ToList();
            }
        }

        public async Task UpdateCustomer(int id, CustomerForUpdateDto customer)
        {
            var query = "UPDATE Customers SET firstName=@firstName,lastName=@lastName where customer_id=@customer_id";

            var parameters = new DynamicParameters();
            parameters.Add("customer_id", id, DbType.Int32);
            parameters.Add("firstName", customer.firstName, DbType.String);
            parameters.Add("lastName", customer.lastName, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}

