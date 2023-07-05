using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;

namespace FoodDelivery.Contracts
{
    public interface IFoodCategoryRepository
    {
        public Task<FoodCategory> GetFoodCategory(int id);
        public Task<FoodCategory> AddFoodCategory(FoodCategoryForAddingDto foodCategory);
        public Task UpdateFoodCategory(int id,FoodCategoryForUpdateDto foodCategory);
    }
}
