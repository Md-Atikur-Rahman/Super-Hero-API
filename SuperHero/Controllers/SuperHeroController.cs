using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>();

        [HttpGet]
        public ActionResult<List<SuperHero>> Get()
        {
            return Ok(heroes);
        }

        [HttpGet("id")]
        public ActionResult<SuperHero> Get(int id)
        {
            var hero = heroes.Find(x => x.Id == id);
            if (hero == null)
                return BadRequest("hero not found");
            return Ok(hero);
        }

        [HttpPost]
        public ActionResult<List<SuperHero>> AddHero(SuperHero hero)
        {
            heroes.Add(hero);
            return Get();
        }

        [HttpPut]
        public ActionResult<string> Update(SuperHero req)
        {
            var hero = heroes.Find(x => x.Id == req.Id);
            if (hero == null)
                return BadRequest("hero not found");
            hero.Name = req.Name;
            hero.FirstName = req.FirstName;
            hero.LastName = req.LastName;
            hero.City = req.City;
            return Ok("Hero updated");
        }

        [HttpDelete]
        public ActionResult<String> Remove(int id)
        {
            var hero = heroes.Find(x => x.Id == id);
            if (hero == null)
                return BadRequest("hero not found");
            heroes.Remove(hero);
            return Ok("hero deleted");
        }
    }
}
