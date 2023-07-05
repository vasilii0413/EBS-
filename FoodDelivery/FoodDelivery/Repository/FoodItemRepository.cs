using System.Data;
using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Contracts;
using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;

namespace FoodDelivery.Repository
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly DapperContext _context;

        public FoodItemRepository(DapperContext context) => _context = context;

        public async Task<FoodItem> GetFoodItem(int id)
        {
            var query = "SELECT foodItem_id as FoodItemId,foodItemName as FoodItemName,foodItemPrice as FoodItemPrice,foodCategory_id as FoodCategoryId FROM " +
                        "FoodItems WHERE foodItem_id=@id";

            using (var connection = _context.CreateConnection())
            {
                var foodItem = await connection.QuerySingleOrDefaultAsync<FoodItem>(query, new { id });
                return foodItem;
            }
        }

        public async Task<FoodItem> GetFoodItemByWordContained(string word)
        {
            var query = "SELECT foodItem_id as FoodItemId,foodItemName as FoodItemName, foodItemPrice as FoodItemPrice, foodCategory_id as FoodCategoryId " +
                "FROM FoodItems WHERE foodItemName LIKE @word + '%'";


            using (var connection = _context.CreateConnection())
            {
                var foodItem = await connection.QuerySingleOrDefaultAsync<FoodItem>(query, new { word });
                return foodItem;
            }
        }

        public async Task UpdateFoodItem(int id, FoodItemForUpdateDto foodItem)
        {
            var query = "UPDATE FoodItems SET foodItemName=@FoodItemName,foodItemPrice=@FoodItemPrice," +
                "foodCategory_id=@FoodCategoryId WHERE foodItem_id=@id";

            var parameters = new DynamicParameters();

            parameters.Add("id",id,DbType.Int32);
            parameters.Add("FoodItemName", foodItem.FoodItemName, DbType.String);
            parameters.Add("FoodItemPrice", foodItem.FoodItemPrice, DbType.Decimal);
            parameters.Add("FoodCategoryId", foodItem.FoodCategoryId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
