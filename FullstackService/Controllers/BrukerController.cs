using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FullstackService.DAL;
using FullstackService.DTO;
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

        [HttpPost("auth", Name = "VerifiserBruker")]
        public async Task<ActionResult> VerifiserBruker([FromBody] BrukerDTO bruker)
        {
            var hentetBruker = await _db.VerifiserBruker(bruker);

            if (hentetBruker is null)
            {
                return Unauthorized("Username or password is wrong");
            }

            return Ok(hentetBruker);
        }

        [HttpPost]
        public async Task<ActionResult> AddBruker([FromBody] BrukerDTO bruker)
        {
            var returBruker = await _db.LeggTil(bruker);
            return CreatedAtRoute(nameof(HentBruker), new {Id = returBruker.Id}, 
                new Bruker{Id = returBruker.Id, Brukernavn = returBruker.Brukernavn});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EndreBruker([FromBody] BrukerDTO bruker, int id)
        {
            return Ok();
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