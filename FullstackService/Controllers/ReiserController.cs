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
        public async Task<ActionResult> GetAllReiserAsync()
        {
            var reiser = await _repo.GetAllReiseAsync();
            return Ok(reiser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetReiseByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            var reise = await _repo.GetReiseByIdAsync(id);
            if (reise == null)
            {
                return BadRequest("Ingen reise funnet");
            }

            return Ok(reise);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOneReiseAsync([FromBody] Reise reise)
        {
            
            if ( await _repo.AddOneReiseAsync(reise))
            {
                return Ok(reise);
            }
            return BadRequest("No reise created");
        }

        [HttpGet("postnummer/{postnummer}")]
        public async Task<ActionResult> HentPostByPostnummer(string postnummer)
        {
            Console.WriteLine($"--> {postnummer}");
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
            
            var dbReise = await _repo.UpdateReiseAsync(reiseId, reise);
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
            
            var reise = await _repo.DeleteReiseAsync(reiseId);
            if (reise is null)
            {
                return BadRequest($"No reise found at: {reiseId}");
            }

            return Ok(reise);
        }
    }
}