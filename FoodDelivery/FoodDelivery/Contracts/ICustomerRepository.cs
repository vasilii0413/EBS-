using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;

namespace FoodDelivery.Contracts
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetCustomers();
        public Task<Customer> AddCustomer(CustomerForAddDto customer);
        public Task UpdateCustomer(int id,CustomerForUpdateDto customer);
        public Task <Customer>GetCustomer(int id);
    }
}
