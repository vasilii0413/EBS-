using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;

namespace FoodDelivery.Contracts
{
    public interface IFoodItemRepository
    {
        public Task<FoodItem> GetFoodItem(int id);
        public Task<FoodItem> GetFoodItemByWordContained(string word);
        public Task UpdateFoodItem(int id, FoodItemForUpdateDto foodItem);
    }
}
