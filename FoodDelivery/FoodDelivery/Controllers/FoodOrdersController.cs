using FoodDelivery.Contracts;
using FoodDelivery.Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/FoodOrders")]
    [ApiController]
    public class FoodOrdersController : ControllerBase
    {
        private readonly IFoodOrderRepository _foodOrderRepo;
        public FoodOrdersController(IFoodOrderRepository foodOrderRepo) => _foodOrderRepo = foodOrderRepo;


        [HttpPost("add")]
        public async Task<IActionResult> AddFoodOrder([FromBody] FoodOrderForAddDto foodOrder)
        {
            var createdFoodOrder = await _foodOrderRepo.AddFoodOrder(foodOrder);
            return CreatedAtRoute("FoodOrderById", new { id = createdFoodOrder.FoodOrderId}, createdFoodOrder);
        }


        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelFoodOrder(int id)
        {
            var dbFoodOrder = await _foodOrderRepo.GetFoodOrder(id);

            if (dbFoodOrder is null)
                return NotFound();

            await _foodOrderRepo.CancelFoodOrder(id);
            return NoContent();
        }


        [HttpPut("setDriver/{id}")]//setDriver=>user
        [Authorize(Roles = "Admin,Driver")]
        public async Task<IActionResult> UpdateFoodOrder(int id, [FromBody] FoodOrderForUpdateDto foodOrder)
        {
            var dbFoodOrder = await _foodOrderRepo.GetFoodOrder(id);

            if (dbFoodOrder is null)
                return NotFound();

            await _foodOrderRepo.UpdateFoodOrder(id, foodOrder);
            return NoContent();
        }
    }
}
