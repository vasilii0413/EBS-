using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;

namespace FoodDelivery.Contracts
{
    public interface IUserRepository
    {
        public Task<User> AddUser(UserForAddDto user);
    }
}
