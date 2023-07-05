using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Contracts;
using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;
using System.Data;

namespace FoodDelivery.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context) => _context = context;

        public async Task<User> AddUser(UserForAddDto user)
        {
            var query = "INSERT INTO Users(firstName,lastName,email,password)values(@firstName,@lastName,@email,@password)"+
                "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("firstName", user.FirstName, DbType.String);
            parameters.Add("lastName",  user.LastName, DbType.String);
            parameters.Add("email", user.Email, DbType.String);
            parameters.Add("password", user.Password, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var addedUser = new User
                {
                    UserId = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                };
                return addedUser;
            }
        }
    }
}
