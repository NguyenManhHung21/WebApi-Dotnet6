using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]



    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero {
                Id = 1,
                Name="Super Man",
                FirstName="Peter",
                LastName="Parker",
                Place="New York City"
            },
            new SuperHero {
                Id = 2,
                Name="Iron Man",
                FirstName="Tony",
                LastName="Stank",
                Place="Long Island"
            }
        };
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await this.context.SuperHeroes.ToListAsync());
        }


        //[HttpGet]
        //public async Task<ActionResult<List<SuperHero>>> GetList()
        //{
        //    return Ok(heroes);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var hero = await this.context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found!");
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult> AddHero(SuperHero hero)
        {
            this.context.SuperHeroes.Add(hero);
            await this.context.SaveChangesAsync(); 
            return Ok(await this.context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult> UpdateHero(SuperHero superHero)
        {
            var dbHero = await this.context.SuperHeroes.FindAsync(superHero.Id);
            if (dbHero == null)
                return BadRequest("Not Found any hero!");
            dbHero.Name = superHero.Name;
            dbHero.FirstName = superHero.FirstName;
            dbHero.LastName = superHero.LastName;
            dbHero.Place = superHero.Place;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.SuperHeroes.ToListAsync());

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dbHero = await this.context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero is not found!");
            
            this.context.SuperHeroes.Remove(dbHero);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.SuperHeroes.ToListAsync());

        }

    }
}
