using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace SuperHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SuperHeroController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbclient = new MongoClient(_configuration.GetConnectionString("ShAppCon"));
            var dblist = dbclient.GetDatabase("testdb").GetCollection<SuperHero>("SuperHero").AsQueryable();
            return new JsonResult(dblist);
        }

        [HttpPost]
        public JsonResult Post(SuperHero hero)
        {
            MongoClient dbclient = new MongoClient(_configuration.GetConnectionString("ShAppCon"));
            dbclient.GetDatabase("testdb").GetCollection<SuperHero>("SuperHero").InsertOne(hero);
            return new JsonResult("Added succesfully");
        }

        [HttpPut]
        public JsonResult Pust(SuperHero hero)
        {
            MongoClient dbclient = new MongoClient(_configuration.GetConnectionString("ShAppCon"));
            var filter = Builders<SuperHero>.Filter.Eq("HeroId", hero.HeroId);
            var update = Builders<SuperHero>.Update.Set("Name", hero.Name)
                                                 .Set("FirstName", hero.FirstName)
                                                 .Set("LastName", hero.LastName)
                                                 .Set("City", hero.City);
            dbclient.GetDatabase("testdb").GetCollection<SuperHero>("SuperHero").UpdateOne(filter, update);
            return new JsonResult("updated succesfully");
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            MongoClient dbclient = new MongoClient(_configuration.GetConnectionString("ShAppCon"));

            var filter = Builders<SuperHero>.Filter.Eq("HeroId", id);
            dbclient.GetDatabase("testdb").GetCollection<SuperHero>("SuperHero").DeleteOne(filter);
            return new JsonResult("Deleted Successfully");
        }
    }
}
