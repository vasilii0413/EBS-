using FoodDelivery.Contracts;
using FoodDelivery.Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace FoodDelivery.Controllers
{
    [Route("customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        public CustomersController(ICustomerRepository customerRepo) => _customerRepo = customerRepo;


        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepo.GetCustomers();
            return Ok(customers);

        }


        [HttpPost("add")]
        public async Task<IActionResult>AddCustomer([FromBody]CustomerForAddDto customer)
        {
            var createdCustomer = await _customerRepo.AddCustomer(customer);
            return CreatedAtRoute("CustomerById", new { id = createdCustomer.customer_id}, createdCustomer);
        }


        [HttpGet("{id}",Name ="CustomerById")]
        public async Task<IActionResult>GetCustomer(int id)
        {
            var customer=await _customerRepo.GetCustomer(id);
            if (customer is null)
                return NotFound();
            return Ok(customer);
        }


        [HttpPut("edit/{id}")]
        public async Task<IActionResult>UpdateCustomer(int id, [FromBody]CustomerForUpdateDto customer)
        {
            var dbCustomer = await _customerRepo.GetCustomer(id);
            if (dbCustomer is null)
                return NotFound();
            await _customerRepo.UpdateCustomer(id, customer);
            return NoContent();
        }
    }
}
