using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Contracts;
using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;
using System.Data;

namespace FoodDelivery.Repository
{
    public class FoodCategoryRepository : IFoodCategoryRepository
    {
        private readonly DapperContext _context;

        public FoodCategoryRepository(DapperContext context) => _context = context;

        public async Task<FoodCategory> AddFoodCategory(FoodCategoryForAddingDto foodCategory)
        {
            var query = "INSERT INTO FoodCategories(foodCategoryName,restaurant_id) " +
                                            "VALUES(@foodCategory,@restaurant_id)" +
                                            "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();

            parameters.Add("foodCategory", foodCategory.FoodCategoryName, DbType.String);
            parameters.Add("restaurant_id", foodCategory.RestaurantId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var CreatedFoodCategory = new FoodCategory
                {
                    FoodCategoryId = id,
                    FoodCategoryName = foodCategory.FoodCategoryName,
                    RestaurantId = foodCategory.RestaurantId
                };
                return CreatedFoodCategory;
            }
        }

        public async Task<FoodCategory> GetFoodCategory(int id)
        {
              var query = "SELECT foodCategory_id as FoodCategoryId,foodCategoryName as FoodCategoryName,Rest.restaurant_id as RestaurantId,Rest.restaurantName as RestName from " +
                "FoodCategories as FoodCategory join Restaurants as Rest" +
                " on FoodCategory.restaurant_id=Rest.restaurant_id where  Rest.restaurant_id=@id";

             using (var connection = _context.CreateConnection())
             {
                 var FoodCategory = await connection.QuerySingleAsync<FoodCategory>(query, new { id });
                 return FoodCategory;
             }
        }

        public async Task UpdateFoodCategory(int id, FoodCategoryForUpdateDto foodCategory)
        {
            var query = "UPDATE FoodCategories set foodCategoryName=@foodCategoryName where foodCategory_id=@foodCategory_id";
            
            var parameters = new DynamicParameters();
            parameters.Add("foodCategory_id", id, DbType.Int32);
            parameters.Add("foodCategoryName", foodCategory.FoodCategoryName, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
