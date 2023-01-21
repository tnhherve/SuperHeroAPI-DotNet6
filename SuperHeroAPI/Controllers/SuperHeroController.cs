using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero
            {
                Id=1,
                Name="Super mane",
                FirstName="Peter",
                LastName="Parker",
                Place="New york City"
            },
            new SuperHero
            {
                Id=2,
                Name="SpiderMan",
                FirstName="Alian",
                LastName="Yann",
                Place="Bathurst"
            },
            new SuperHero
            {
                Id=3,
                Name="Iron man",
                FirstName="Tony",
                LastName="Stark",
                Place="Bafoussam"
            }
        };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            SuperHero hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero super)
        {
            var hero = await _context.SuperHeroes.FindAsync(super.Id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            hero.Name = super.Name;
            hero.FirstName = super.FirstName;
            hero.LastName = super.LastName;
            hero.Place = super.Place;
            //_context.SuperHeroes.Update(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> DeleteHero(int id)
        {
            SuperHero hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
