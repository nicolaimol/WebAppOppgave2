using System.Threading.Tasks;
using FullstackService.DAL;
using FullstackService.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullstackService.Controllers
{
    [ApiController]
    [Route("api/bruker")]
    public class BrukerController:ControllerBase
    {
        private readonly IBrukerRepo _db;

        public BrukerController(IBrukerRepo db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult> HentAlle()
        {
            var brukere = _db.HentAlle();
            return Ok(brukere);
        }

        [HttpGet("{id}", Name = "HentBruker")]
        public async Task<ActionResult> HentBruker(int id)
        {
            var bruker = await _db.HentEn(id);
            if (bruker != null)
            {
                return Ok(bruker);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> AddBruker([FromBody] Bruker bruker)
        {
            var returBruker = await _db.LeggTil(bruker);
            return CreatedAtRoute(nameof(HentBruker), new {Id = returBruker.Id}, returBruker);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EndreBruker([FromBody] Bruker bruker, int id)
        {
            var brukeren = _db.HentEn(id).Result;

            if (brukeren != null)
            {
                brukeren.Id = bruker.Id;
                brukeren.Brukernavn = bruker.Brukernavn;
                brukeren.PassordHash = bruker.PassordHash;
            } else
            {
                BadRequest($"No bruker found by id {id}");
            }

            int lagre = await _db.Lagre();
            if (lagre > 0)
            {
                return Ok(bruker);
            }
            return BadRequest("No changes made");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBruker(int id)
        {
            if (await _db.Slett(id) >= 0)
            {
                return NoContent();
            }

            return BadRequest("No changes made");
        }

    }
}