using System;
using System.Threading.Tasks;
using FullstackService.DAL;
using FullstackService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FullstackService.Controllers
{
    [ApiController]
    [Route("api/bestilling")]
    public class BestillingsController:ControllerBase
    {
        
        private readonly IBestillingRepo _db;
        private readonly ILogger<BestillingsController> _log;
        private const string _loggetInn = "logget inn";

        public BestillingsController(IBestillingRepo db, ILogger<BestillingsController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public async Task<ActionResult> HentAlleBestillingerAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            _log.LogInformation("HentAlle()");
            var bestillinger = await _db.HentAlleBestillingerAsync();
            return Ok(bestillinger);
        }

        [HttpGet("{id}", Name ="HentBestillingByIdAsync")]
        public async Task<ActionResult> HentBestillingByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            _log.LogInformation($"HentEn({id})");
            var bestilling = await _db.HentEnBestillingAsync(id);
            if (bestilling != null)
            {
                return Ok(bestilling);
            }
            
            return NotFound();
        }
        
        [HttpGet("ref/{referanse}", Name = "HentBestillingByRefAsync")]
        public async Task<ActionResult> HentBestillingByRefAsync(string referanse)
        {
            var bestilling = await _db.HentEnBestillingByRefAsync(referanse);
            if (bestilling != null)
            {
                _log.LogInformation($"HentBestillingByRef({referanse})");
                return Ok(bestilling);
            }

            return NotFound($"Bestilling ikke funnet på referanse: {referanse}");
        }

        [HttpPost]
        public async Task<ActionResult> AddBestillingAsync([FromBody]Bestilling bestilling)
        {
            _log.LogInformation($"AddBestilling({bestilling})");
            bestilling.Referanse = Guid.NewGuid().ToString().Split("-")[0];
            var returBestilling = await _db.LeggTilBestillingAsync(bestilling);

            return CreatedAtRoute(nameof(HentBestillingByRefAsync), 
                new {Referanse = returBestilling.Referanse}, 
                returBestilling);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditBestillingAsync([FromBody] Bestilling bestilling, int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var endretBestilling = await _db.EndreBestillingAsync(id, bestilling);
            if (endretBestilling is not null)
            {
                return Ok(endretBestilling);
            }
            else
            {
                return BadRequest($"No bestilling found with id {id}");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBestillingAsync(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            if (await _db.SlettBestillingAsync(id) >= 0)
            {
                return Ok();
            }

            return BadRequest("No changes made");
        }

    }
}