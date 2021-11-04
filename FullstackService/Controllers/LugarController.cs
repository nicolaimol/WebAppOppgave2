using System.Threading.Tasks;
using FullstackService.DAL;
using FullstackService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullstackService.Controllers
{
    [ApiController]
    [Route("/api/reise/lugar")]
    public class LugarController: ControllerBase
    {
        private readonly ILugarRepo _repo;
        private const string _loggetInn = "logget inn";

        public LugarController(ILugarRepo repo)
        {
            _repo = repo;
        }
        
        [HttpGet("{reiseId}"), ActionName("HentLugerByReise")]
        public async Task<ActionResult> HentLugerByReiseAsync(int reiseId)
        {
            return Ok(await _repo.HentLugerByReiseAsync(reiseId));
        }

        [HttpPost]
        public async Task<ActionResult> CreateLugar([FromBody] Lugar lugar)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            return Ok(await _repo.CreateLugarAsync(lugar));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLugar([FromBody] Lugar lugar)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            var dbLugar = await _repo.UpdateLugarAsync(lugar);
            if (dbLugar is null)
            {
                return BadRequest("No lugar with id");
            }
            
            return Ok(dbLugar);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLugarAsync(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            var lugar = await _repo.DeleteLugarAsync(id);
            if (lugar is null) return BadRequest();

            return Ok(lugar);
        }
    }
}