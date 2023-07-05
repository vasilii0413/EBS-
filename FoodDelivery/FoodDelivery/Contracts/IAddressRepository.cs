using FoodDelivery.Entities;
using FoodDelivery.Contracts;
using FoodDelivery.Context;
using FoodDelivery.Entities.Dto;

namespace FoodDelivery.Contracts
{
    public interface IAddressRepository
    {
        public Task<Addresses> GetAddresses(int id);
        public Task UpdateAddress(int id,AddressForUpdateDto address);
    }
}
