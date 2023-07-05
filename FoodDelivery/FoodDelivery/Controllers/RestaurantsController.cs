using Dapper;
using FoodDelivery.Contracts;
using FoodDelivery.Entities;
using FoodDelivery.Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FoodDelivery.Controllers
{
    [Route("restaurants")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepo;

        public RestaurantsController(IRestaurantRepository restaurantRepo) => _restaurantRepo = restaurantRepo;


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _restaurantRepo.GetRestaurants();
            return Ok(restaurants);

        }


        [HttpPost("add")]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantForCreation restaurant)
        {
            var createdRestaurant = await _restaurantRepo.CreateRestaurant(restaurant);
            return CreatedAtRoute("RestaurantById", new { id = createdRestaurant.restaurant_id }, createdRestaurant);
        }


        [HttpGet("{id}", Name = "RestaurantById")]
        public async Task<IActionResult> GetRestaurant(int id)
        {
            var restaurant = await _restaurantRepo.GetRestaurant(id);
            if (restaurant is null)
                return NotFound();
            return Ok(restaurant);
        }


        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantForUpdateDto restaurant)
        {
            var dbRestaurant = await _restaurantRepo.GetRestaurant(id);
            if (dbRestaurant is null)
                return NotFound();

            await _restaurantRepo.UpdateRestaurant(id, restaurant);
            return NoContent();
        }

        [HttpGet("menu/id")]
        public IEnumerable<FoodItem> GetMenu(int id)
        {
            using (var connection = new SqlConnection(@"Data Source=DESKTOP-07K3JBC;Initial Catalog=FoodDelivery;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"))
            {
                connection.Open();

                var sql = @"
                SELECT
                    Restaurants.restaurantName,
                    FoodCategories.foodCategoryName,
                    FoodItems.foodItemName,
                    FoodItems.foodItemPrice
                FROM
                    Restaurants
                    JOIN FoodCategories ON Restaurants.restaurant_id = FoodCategories.restaurant_id
                    JOIN FoodItems ON FoodCategories.foodCategory_id = FoodItems.foodCategory_id
                WHERE
                    Restaurants.restaurant_id = @RestaurantId;
            ";

                var menu = connection.Query<FoodItem>(sql, new { RestaurantId = id });

                return menu.ToList();
            }
        }
    }
}
