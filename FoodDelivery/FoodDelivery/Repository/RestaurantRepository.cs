using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Contracts;
using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;
using System.Data;
using System.Net;

namespace FoodDelivery.Repository
{
    public class RestaurantRepository:IRestaurantRepository
    {
        private readonly DapperContext _context;

        public RestaurantRepository(DapperContext context) => _context = context;

        public async Task<Restaurant> CreateRestaurant(RestaurantForCreation restaurant)
        {
            var query = "INSERT INTO Restaurants(restaurantName,address_id)VALUES(@name,@address_id)" +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("name", restaurant.name,DbType.String);
            parameters.Add("address_id", restaurant.address_id,DbType.Int64);

            using (var connection=_context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdRestaurant = new Restaurant
                {
                    restaurant_id=id,
                    name = restaurant.name,
                    address_id = restaurant.address_id,

                };
                return createdRestaurant;
            }

        }

        public async Task<Restaurant> GetRestaurant(int id)
        {
            var query = @"SELECT r.restaurant_id, r.restaurantName as name, r.address_id,
                    a.address_id, a.houseNumber, a.streetName, a.city, a.postalCode
                FROM Restaurants r
                INNER JOIN Addresses a ON r.address_id = a.address_id
                WHERE r.restaurant_id = @id";

            using (var connection = _context.CreateConnection())
            {
                var restaurantDictionary = new Dictionary<int, Restaurant>();
                var restaurantList = await connection.QueryAsync<Restaurant, Addresses, Restaurant>(
                    query,
                    (restaurant, address) =>
                    {
                        if (!restaurantDictionary.TryGetValue(restaurant.restaurant_id, out var restaurantEntry))
                        {
                            restaurantEntry = restaurant;
                            restaurantEntry.addresses = new List<Addresses> { address };
                            restaurantDictionary.Add(restaurantEntry.restaurant_id, restaurantEntry);
                        }
                        else
                        {
                            restaurantEntry.addresses.Add(address);
                        }
                        return restaurantEntry;
                    },
                    new { id },
                    splitOn: "address_id"
                );

                return restaurantList.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurants()
        {
            var query = "SELECT restaurant_id,restaurantName as name,address_id FROM Restaurants";
            using (var connection=_context.CreateConnection())
            {
                var restaurants=await connection.QueryAsync<Restaurant>(query);

                return restaurants.ToList();
            }
        }

        public async Task UpdateRestaurant(int id, RestaurantForUpdateDto restaurant)
        {
            var query = "UPDATE Restaurants set restaurantName=@restaurantName where restaurant_id=@restaurant_id";
            var parameters = new DynamicParameters();
            parameters.Add("restaurant_id", id, DbType.Int32);
            parameters.Add("restaurantName", restaurant.name, DbType.String);

            using (var connection=_context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }
    }
}
