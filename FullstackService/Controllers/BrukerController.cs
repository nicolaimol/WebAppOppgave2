using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FullstackService.DAL;
using FullstackService.DTO;
using FullstackService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullstackService.Controllers
{
    [ApiController]
    [Route("api/bruker")]
    public class BrukerController:ControllerBase
    {
        private readonly IBrukerRepo _db;

        private const string _loggetInn = "logget inn";

        public BrukerController(IBrukerRepo db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult> HentAlle()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            var brukere = await _db.HentAlleBrukereAsync();
            return Ok(brukere);
        }

        [HttpGet("{id}", Name = "HentBruker")]
        public async Task<ActionResult> HentBruker(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            var bruker = await _db.HentEnBrukerByIdAsync(id);
            if (bruker != null)
            {
                return Ok(bruker);
            }
            return NotFound();
        }

        [HttpPost("auth", Name = "VerifiserBruker")]
        public async Task<ActionResult> VerifiserBruker([FromBody] BrukerDTO bruker)
        {
            var hentetBruker = await _db.VerifiserBrukerAsync(bruker);

            if (hentetBruker is null)
            {
                HttpContext.Session.SetString(_loggetInn, "");
                return Unauthorized("Username or password is wrong");
            }
            HttpContext.Session.SetString(_loggetInn, hentetBruker.Brukernavn);
            return Ok(hentetBruker);
        }

        [HttpGet("out")]
        public ActionResult LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
            return Ok();
        }

        [HttpGet("auth")]
        public ActionResult AutoriserBruker()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddBruker([FromBody] BrukerDTO bruker)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            var returBruker = await _db.LeggTilBrukerAsync(bruker);
            return CreatedAtRoute(nameof(HentBruker), new {Id = returBruker.Id}, 
                new Bruker{Id = returBruker.Id, Brukernavn = returBruker.Brukernavn});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EndreBruker([FromBody] BrukerDTO bruker, int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBruker(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (await _db.SlettBrukerAsync(id) >= 0)
            {
                return NoContent();
            }

            return BadRequest("No changes made");
        }


    }
}