using FoodDelivery.Contracts;
using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepo;
        public AddressController(IAddressRepository addressRepo) => _addressRepo = addressRepo;


        [HttpGet("{id}",Name = "AddressById")]
        public async Task<IActionResult>GetAddress(int id)
        {
            var address = await _addressRepo.GetAddresses(id);
            if (address is null)
                return NotFound();
            return Ok(address);

        }


        [HttpPut("edit/{id}")]
        public async Task<IActionResult>UpdateAddress(int id, [FromBody]AddressForUpdateDto address)
        {
            var dbAddress = await _addressRepo.GetAddresses(id);

            if (dbAddress is null)
                return NotFound();

            await _addressRepo.UpdateAddress(id, address);
            return NoContent();
        }
    }
}
