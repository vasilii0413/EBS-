using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Contracts;
using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;
using System.Data;

namespace FoodDelivery.Repository
{
    public class FoodOrderRepository : IFoodOrderRepository
    {
        private readonly DapperContext _context;

        public FoodOrderRepository(DapperContext context) => _context = context;

        public async Task<FoodOrder> AddFoodOrder(FoodOrderForAddDto foodOrder)
        {
            var query = "INSERT INTO FoodOrders(customer_id,address_id,user_id,orderStatus_id,restaurant_id,deliveryFee," +
                "                               totalAmount,orderDateTime,requestedDeliveryDateTime)" +
                "VALUES(@customer_id,@address_id,@user_id,@orderStatus_id,@restaurant_id,@deliveryFee,@totalAmount,@orderDateTime," +
                "@requestedDeliveryDateTime)" +
                        "SELECT CAST(SCOPE_IDENTITY() AS int)"; ;

            var parameters = new DynamicParameters();
            parameters.Add("customer_id", foodOrder.CustomerId, DbType.Int32);
            parameters.Add("address_id", foodOrder.AddressId, DbType.Int32);
            parameters.Add("user_id", foodOrder.UserId, DbType.Int32);
            parameters.Add("orderStatus_id", foodOrder.OrderStatusId, DbType.Int32);
            parameters.Add("restaurant_id", foodOrder.RestaurantId, DbType.Int32);
            parameters.Add("deliveryFee", foodOrder.DeliveryFee, DbType.Decimal);
            parameters.Add("totalAmount", foodOrder.TotalAmount, DbType.Decimal);
            parameters.Add("orderDateTime", foodOrder.OrderDateTime, DbType.DateTime);
            parameters.Add("requestedDeliveryDateTime", foodOrder.RequestedDeliveryOrderDate, DbType.DateTime);
            

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var addedFoodOrder = new FoodOrder
                {
                    FoodOrderId = id,
                    CustomerId = foodOrder.CustomerId,
                    UserId = foodOrder.UserId,
                    OrderStatusId = foodOrder.OrderStatusId,
                    RestaurantId =  foodOrder.RestaurantId,
                    DeliveryFee = foodOrder.DeliveryFee,
                    TotalAmount = foodOrder.TotalAmount,
                    OrderDateTime = foodOrder.OrderDateTime,
                    RequestedDeliveryOrderDate = foodOrder.RequestedDeliveryOrderDate,
                    
                };
                return addedFoodOrder;

            }
        }

        public async Task CancelFoodOrder(int id)
        {
            var query = "DELETE FROM FoodOrders where foodOrder_id=@id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new {id});
            }
        }

        public async Task<FoodOrder> GetFoodOrder(int id)
        {
            var query = "SELECT * FROM FoodOrders where foodOrder_id=@id";

            using (var connection = _context.CreateConnection())
            {
                var foodOrder = await connection.QuerySingleAsync<FoodOrder>(query, new { id });
                return foodOrder;
            }
        }

        public async Task UpdateFoodOrder(int id,FoodOrderForUpdateDto foodOrder)
        {
            var query = "UPDATE FoodOrders SET user_id=@UserId where foodOrder_id=@id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id,DbType.Int32);
            parameters.Add("UserId",foodOrder.UserId,DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
