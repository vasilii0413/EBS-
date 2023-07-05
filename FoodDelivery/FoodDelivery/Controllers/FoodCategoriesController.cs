using FoodDelivery.Contracts;
using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/FoodCategory")]
    [ApiController]
    public class FoodCategoriesController : ControllerBase
    {

        private readonly IFoodCategoryRepository _foodCategoryRepo;
        public FoodCategoriesController(IFoodCategoryRepository foodCategoryRepo) => _foodCategoryRepo = foodCategoryRepo;


        [HttpGet("{id}", Name = "FoodCategoryById")]
        public async Task<IActionResult> GetFoodCategory(int id)
        {
            var FoodCategory = await _foodCategoryRepo.GetFoodCategory(id);
            if (FoodCategory is null)
                return NotFound();
            return Ok(FoodCategory);
        }

        
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddFoodCategory([FromBody]FoodCategoryForAddingDto foodCategory)
        {
            var addedFoodCategory = await _foodCategoryRepo.AddFoodCategory(foodCategory);

            return CreatedAtRoute("FoodCategoryById", new { id = addedFoodCategory.FoodCategoryId},addedFoodCategory);
        }


        [HttpPut("edit/{id}")]
        public async Task<IActionResult>UpdateFoodCategory(int id, [FromBody]FoodCategoryForUpdateDto foodCategory)
        {
            var dbFoodCategory = await _foodCategoryRepo.GetFoodCategory(id);
            if (dbFoodCategory is null)
                return NotFound();

            await _foodCategoryRepo.UpdateFoodCategory(id, foodCategory);
            return NoContent();
        }
    }
}
