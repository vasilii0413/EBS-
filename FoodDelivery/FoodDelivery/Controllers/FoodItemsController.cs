using FoodDelivery.Contracts;
using FoodDelivery.Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/FoodItems")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly IFoodItemRepository _foodItemRepo;
        public FoodItemsController(IFoodItemRepository itemRepo) => _foodItemRepo = itemRepo;


        [HttpGet("{id}", Name = "FoodItemById")]
        public async Task<IActionResult> GetFoodItem(int id)
        {
            var foodItem = await _foodItemRepo.GetFoodItem(id);
            if (foodItem is null)
                return NotFound();

            return Ok(foodItem);

        }


        [HttpGet("search/{word}", Name = "FoodItemBySearchArgument")]
        public async Task<IActionResult> GetFoodItemByWordContained(string word)
        {
            var foodItem = await _foodItemRepo.GetFoodItemByWordContained(word);
            if (foodItem is null)
                return NotFound();

            return Ok(foodItem);

        }


        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateFoodItem(int id, [FromBody] FoodItemForUpdateDto foodItem)
        {
            var dbFoodItem = await _foodItemRepo.GetFoodItem(id);
            if (dbFoodItem is null)
                return NotFound();

            await _foodItemRepo.UpdateFoodItem(id, foodItem);
            return NoContent();
        }
    }
}
