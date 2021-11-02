using System;
using System.Threading.Tasks;
using FullstackService.DAL;
using FullstackService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullstackService.Controllers
{
    [ApiController]
    [Route("api/reise")]
    public class ReiserController: ControllerBase
    {
        private readonly IReiseRepo _repo;
        private const string _loggetInn = "logget inn";
        
        public ReiserController(IReiseRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var reiser = await _repo.GetAll();
            return Ok(reiser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetReiseById(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            var reise = await _repo.GetOneById(id);
            if (reise == null)
            {
                return BadRequest("Ingen reise funnet");
            }

            return Ok(reise);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOne([FromBody] Reise reise)
        {
            
            if ( await _repo.AddOne(reise))
            {
                return Ok(reise);
            }
            return BadRequest("No reise created");
        }

        [HttpGet("lugar/{reiseId}"), ActionName("HentLugerByReise")]
        public async Task<ActionResult> HentLugerByReise(int reiseId)
        {
            return Ok(await _repo.HentLugerByReise(reiseId));
        }

        [HttpPost("lugar")]
        public async Task<ActionResult> CreateLugar([FromBody] Lugar lugar)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            return Ok(await _repo.CreateLugar(lugar));
        }

        [HttpPut("lugar")]
        public async Task<ActionResult> UpdateLugar([FromBody] Lugar lugar)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            var dbLugar = await _repo.UpdateLugar(lugar);
            if (dbLugar is null)
            {
                return BadRequest("No lugar with id");
            }
            
            return Ok(dbLugar);
        }

        [HttpGet("postnummer/{postnummer}")]
        public async Task<ActionResult> HentPostByPostnummer(string postnummer)
        {
            var poststed = await _repo.HentPoststedByPostnummer(postnummer);
            if (poststed != null)
            {
                return Ok(poststed);
            }

            return BadRequest();
        }

        [HttpPut("{reiseId}")]
        public async Task<ActionResult> UpdateReise(int reiseId, [FromBody] Reise reise)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            var dbReise = await _repo.UpdateReise(reiseId, reise);
            if (dbReise is null)
            {
                return BadRequest($"No reise at id {reiseId}");
            }

            return Ok(dbReise);
        }

        [HttpDelete("{reiseId}")]
        [AcceptVerbs("DELETE")]
        public async Task<ActionResult> DeleteReise(int reiseId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            var reise = await _repo.DeleteReise(reiseId);
            if (reise is null)
            {
                return BadRequest($"No reise found at: {reiseId}");
            }

            return Ok(reise);
        }
    }
}