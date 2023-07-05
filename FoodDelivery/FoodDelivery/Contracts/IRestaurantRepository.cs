using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;

namespace FoodDelivery.Contracts
{
    public interface IRestaurantRepository
    {
        public Task<IEnumerable<Restaurant>> GetRestaurants();
        public Task<Restaurant> CreateRestaurant(RestaurantForCreation restaurant);
        public Task<Restaurant> GetRestaurant(int id);
        public Task UpdateRestaurant(int id, RestaurantForUpdateDto restaurant);
        
    }
}
