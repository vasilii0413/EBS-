using FoodDelivery.Contracts;
using FoodDelivery.Entities.Dto;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo) => _userRepo = userRepo;

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser([FromBody]UserForAddDto user)
        {
            var addedUser = await _userRepo.AddUser(user);

            return CreatedAtRoute("UserById", new { id = addedUser.UserId }, addedUser);
        }
    }
}
