using FoodDelivery.Entities.Dto;
using FoodDelivery.Entities;

namespace FoodDelivery.Contracts
{
    public interface IFoodOrderRepository
    {
        public Task<FoodOrder> AddFoodOrder(FoodOrderForAddDto foodOrder);
        public Task CancelFoodOrder(int id);
        public Task<FoodOrder>GetFoodOrder(int id);
        public Task UpdateFoodOrder(int id,FoodOrderForUpdateDto foodOrder);

    }
}
