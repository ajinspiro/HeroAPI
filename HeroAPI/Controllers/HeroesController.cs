using HeroAPI.DataAccess;
using HeroAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HeroAPI.Controllers
{
    [ApiController]
    //[EnableCors(PolicyName = "AllowAll")]
    [Route("api/heroes")]
    public class HeroesController : ControllerBase
    {
        private readonly ILogger<HeroesController> _logger;
        private readonly HeroDbContext _heroDbContext;

        public HeroesController(ILogger<HeroesController> logger, HeroDbContext heroDbContext)
        {
            _logger = logger;
            _heroDbContext = heroDbContext;
        }

        [HttpGet("")]
        public List<Hero> GetAll() => _heroDbContext.Heroes.ToList();

        [HttpGet("{heroId:int}")]
        public IActionResult Get(int heroId)
        {
            if (heroId < 1) return BadRequest();

            var hero = _heroDbContext.Heroes.Find(heroId);
            if (hero is null) return NotFound();

            return Ok(hero);
        }

        [HttpPut("{heroId:int}")]
        public IActionResult Put(int heroId, [FromBody] Hero heroToUpdate)
        {
            if (heroId < 1) return BadRequest();

            var hero = _heroDbContext.Heroes.Find(heroId);
            if (hero is null) return NotFound();

            hero.Name = heroToUpdate.Name;
            _heroDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{heroId:int}")]
        public IActionResult Delete(int heroId)
        {
            if (heroId < 1) return BadRequest();

            var hero = _heroDbContext.Heroes.Find(heroId);
            if (hero is null) return NotFound();

            _heroDbContext.Heroes.Remove(hero);
            _heroDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            // delete all heroes
            _heroDbContext.Heroes.RemoveRange(_heroDbContext.Heroes.ToList());

            // re-seed heroes
            var list = new List<Hero>
            {
                new Hero { Id = 1, Name = "Superman" },
                new Hero { Id = 2, Name = "Ironman" },
                new Hero { Id = 3, Name = "Batman" },
                new Hero { Id = 4, Name = "Wonder Woman" },
                new Hero { Id = 5, Name = "Ms Marvel" },
                new Hero { Id = 6, Name = "Captain America" },
                new Hero { Id = 7, Name = "Red Tornado" }
            };

            _heroDbContext.Heroes.AddRange(list);

            _heroDbContext.SaveChanges();

            return Ok();
        }
    }
}