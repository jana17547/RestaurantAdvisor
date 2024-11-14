using Neo4j.Driver;
using Server.Models;
using Neo4j.Driver.Extensions;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Services
{
    public class DataProvider
    {
        private IAsyncSession Session{ get; set; }
        public DataProvider()
        {
            Session = new Neo4JDriver().Session;
        }

        public async Task<Restaurant> GetRestaurant(string name, string username)
        {
            var a = (await (await Session.RunAsync($"Match (b:Restaurant) where b.name = '{name}' merge(u: User {{ username: '{username}'}})" +
                $"merge(u)-[r: Visited]->(b) on create set r.visits = 1 on match set r.visits = r.visits + 1 return b")).ToListAsync()).FirstOrDefault();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Restaurant b = a["b"].As<INode>().ToObject<Restaurant>();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return b;
        }

        public async Task<bool> LogInAsync(User user)
        {
            try
            {
                var list = await (await Session.RunAsync($"Merge (u:User {{username: '{user.Username}', password: '{user.Password}'}})  return u")).ToListAsync();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
       
        public async Task<bool> Review(string username, string restaurant, Review r)
        {
            try
            {
                await Session.RunAsync($"Match (b:Restaurant) where b.name = '{restaurant}' merge(u: User {{ username: '{username}'}})" +
                    $"merge(u)-[r: Reviewed]->(b) set r.review = '{r.review}' set r.rating = { r.Rating}");
                ReRate(restaurant);

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async void ReRate(string name)
        {
            await Session.RunAsync($"MATCH ()-[r:Reviewed]->(b:Restaurant) where b.name = '{name}' with b, avg(r.rating) as d set b.rating = d");
        }

        public async Task<List<Review>> GetUsernameReviews(string username)
        {
            List<Review> reviews = new List<Review>();
            var list = await (await Session.RunAsync($"Match (u:User)-[r:Reviewed]->(b:Restaurant) where u.username = '{username}' return r, b.name limit 5")).ToListAsync();
            foreach (var item in list)
            {
                reviews.Add(item["r"].As<IRelationship>().ToObject<Review>());
                reviews.Last<Review>().Name = item["b.name"].As<string>();
            }
            return reviews;
        }
        public async Task<List<Review>> UpdateReviews(string username,string resName,string com)
        {
            List<Review> reviews=new List<Review>();
            var list=await(await Session.RunAsync($"Match (n:User) -[rel:Reviewed]->(b:Restaurant) where n.username='{username}' and b.name='{resName}' SET rel.review='{com}' RETURN rel,b.name limit 5")).ToListAsync();
            foreach (var item in list)
            {
                reviews.Add(item["rel"].As<IRelationship>().ToObject<Review>());
                reviews.Last<Review>().Name = item["b.name"].As<string>();
            }
            return reviews;
        }
        public async Task<List<Review>> GetRestaurantReviews(string name)
        {
            List<Review> reviews = new List<Review>();
            var list = await (await Session.RunAsync($"Match (u:User)-[r:Reviewed]->(b:Restaurant) where b.name = '{name}' return r, u.username limit 5")).ToListAsync();
            foreach (var item in list)
            {
                reviews.Add(item["r"].As<IRelationship>().ToObject<Review>());
                reviews.Last<Review>().Name = item["u.username"].As<string>();
            }
            return reviews;
        }

        public async Task<List<Restaurant>> SearchRestaurant(string tag)
        {
            List<Restaurant> restaurant = new List<Restaurant>();
            var list = await(await Session.RunAsync($"Match (b:Restaurant)-[:tagged]->(t:tag) where t.tag = '{tag}' return b order by b.rating desc limit 5")).ToListAsync();
            foreach (var item in list)
            {
                restaurant.Add(item["b"].As<INode>().ToObject<Restaurant>());
            }
            return restaurant;
        }

        public async Task<List<Restaurant>> GetRecommended(string username)
        {
            List<Restaurant> restaurant = new List<Restaurant>();
           // var list = await (await Session.RunAsync($"Match (u:User)-[v:Visited]->(b:Restaurant) where u.username = '{username}' return b order by v.visits desc limit 5")).ToListAsync();
           // if (list.Count == 0)
             var list = await (await Session.RunAsync($"Match (b:Restaurant) return b order by b.rating desc limit 5")).ToListAsync();
            foreach (var item in list)
            {
                restaurant.Add(item["b"].As<INode>().ToObject<Restaurant>());
            }
            return restaurant;
        }
          public async Task<List<City>>  GetCity()
        {   
             List<City> city=new List<City>();
             var list=await(await Session.RunAsync($"MATCH(c:City )  return c")).ToListAsync() ;
             foreach(var item in list)
             {
                city.Add(item["c"].As<INode>().ToObject<City>());
             }
             return city;
            
        }
        public async Task<List<TypeKitchen>> GetTypeKitchen()
        {   
             List<TypeKitchen> type=new List<TypeKitchen>();
             var list=await(await Session.RunAsync($"MATCH(t:Type )  return t")).ToListAsync() ;
             foreach(var item in list)
             {
                 type.Add(item["t"].As<INode>().ToObject<TypeKitchen>());
             }
             return type;
            
        }
        public async Task<List<Restaurant>> GetRestaurantType1(string city,string type)
        {   
             List<Restaurant> b=new List<Restaurant>();
             var list=await(await Session.RunAsync($"MATCH (c:City)-[r:Contains]->(b:Restaurant)  MATCH(t:Type)-[k:typeKitchen]->(b:Restaurant)  where c.nameCity='{city}' and t.type='{type}' return b")).ToListAsync() ;
             foreach(var item in list)
             {
                 b.Add(item["b"].As<INode>().ToObject<Restaurant>());
             }
             return b;
            
        }
        public async Task<List<Restaurant>> GetRestaurantCity(string nameCity)
        {
            List<Restaurant> b=new List<Restaurant>();
            var list=await (await Session.RunAsync($"Match (c:City)-[r:Contains]->(b:Restaurant) where c.nameCity ='{nameCity}'  return b")).ToListAsync();
            foreach(var item in list)
            {
                b.Add(item["b"].As<INode>().ToObject<Restaurant>());

            }
            return b;
        }
         public async Task<List<Restaurant>> GetRestaurantType(string type)
        {
            List<Restaurant> b=new List<Restaurant>();
            var list=await (await Session.RunAsync($"Match (t:Type)-[k:typeKitchen]->(b:Restaurant) where t.type ='{type}'  return b")).ToListAsync();
            foreach(var item in list)
            {
                b.Add(item["b"].As<INode>().ToObject<Restaurant>());

            }
            return b;
        }
        public void DeleteReview(string username, string name)
        {
            Session.RunAsync($"MATCH(n {{ username: '{username}'}})-[r: Reviewed]->(b: Restaurant {{ name: '{name}'}}) DELETE r");
            ReRate(name);
        }
    }
}