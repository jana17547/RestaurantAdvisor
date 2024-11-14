using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using Server.Services;
using Server.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        public DataProvider data { get; set; }

        public RestaurantController()
        {
            data = new DataProvider();
        }
        [HttpGet]
        [Route("GetCity")]
        public async Task<List<City>> GetCity()
        {
            return await data.GetCity();
        }

        [HttpGet]
        [Route("GetTypeKitchen")]
        public async Task<List<TypeKitchen>> GetTypeKitchens()
        {
            return await data.GetTypeKitchen();
        }
        [HttpGet]
        [Route("GetRestaurantCity/{nameCity}")]
         public async Task<List<Restaurant>> GetRestaurantCity(string nameCity)
         {
            return await data.GetRestaurantCity(nameCity);
         }

          [HttpGet]
        [Route("GetRestaurantType/{type}")]
         public async Task<List<Restaurant>> GetRestaurantType(string type)
         {
            return await data.GetRestaurantType(type);
         }
        [HttpGet]
        [Route("GetRestaurantType1/{city}/{type}")]
         public async Task<List<Restaurant>> GetRestaurantType1(string city,string type)
         {
            return await data.GetRestaurantType1(city,type);
         }
        [HttpGet]
        [Route("GetRestaurant/{name}&{username}")]
        public async Task<Restaurant> GetRestaurant(string name, string username)
        {
            return await data.GetRestaurant(name, username);
        }

        [HttpGet]
        [Route("GetRecommended/{username}")]
        public async Task<List<Restaurant>> GetRecommended(string username)
        {
            return await data.GetRecommended(username);
        }
        [HttpPut]
        [Route("UpdateReview/{username}/{resName}/{com}")]
        public async Task<List<Review>> UpdateReviews(string username,string resName,string com)
        {
            return await data.UpdateReviews(username,resName,com);
        }
        [HttpGet]
        [Route("GetUsernameReviews/{username}")]
        public async Task<List<Review>> GetUsernameReviews(string username)
        {
            return await data.GetUsernameReviews(username);
        }

        [HttpGet]
        [Route("GetRestaurantReviews/{name}")]
        public async Task<List<Review>> GetRestaurantReviews(string name)
        {
            return await data.GetRestaurantReviews(name);
        }

        [HttpGet]
        [Route("SearchRestaurant/{tag}")]
        public async Task<List<Restaurant>> SearchRestaurant(string tag)
        {
            return await data.SearchRestaurant(tag);
        }

        [HttpPost]
        [Route("Review/{username}&{name}")]
        public async Task<IActionResult> ReviewAsync([FromBody] Review r, string username, string name)
        {
            await data.Review(username, name, r);
            return Ok();
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<bool> LogIn([FromBody] User user)
        {
            return await data.LogInAsync(user);
        }

        [HttpDelete]
        [Route("DeleteReview/{username}&{name}")]
        public IActionResult DeleteReview(string username, string name)
        {
            data.DeleteReview(username, name);
            return Ok();
        }
    }
}
