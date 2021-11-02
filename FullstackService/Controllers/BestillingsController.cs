﻿using System;
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

        public BestillingsController(IBestillingRepo db, ILogger<BestillingsController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public async Task<ActionResult> HentAlleBestillingerAsync()
        {
            _log.LogInformation("HentAlle()");
            var bestillinger = await _db.HentAlle();
            return Ok(bestillinger);
        }

        [HttpGet("{id}", Name ="HentBestillingByIdAsync")]
        public async Task<ActionResult> HentBestillingByIdAsync(int id)
        {
            _log.LogInformation($"HentEn({id})");
            var bestilling = await _db.HentEn(id);
            if (bestilling != null)
            {
                return Ok(bestilling);
            }
            
            return NotFound();
        }
        
        [HttpGet("ref/{referanse}", Name = "HentBestillingByRefAsync")]
        public async Task<ActionResult> HentBestillingByRefAsync(string referanse)
        {
            var bestilling = await _db.HentEnByRef(referanse);
            if (bestilling != null)
            {
                _log.LogInformation($"HentBestillingByRef({referanse})");
                return Ok(bestilling);
            }

            return NotFound($"Bestilling ikke funnet på referanse: {referanse}");
        }

        [HttpPost]
        public async Task<ActionResult> AddBestilling([FromBody]Bestilling bestilling)
        {
            _log.LogInformation($"AddBestilling({bestilling})");
            bestilling.Referanse = Guid.NewGuid().ToString().Split("-")[0];
            var returBestilling = await _db.LeggTil(bestilling);

            return CreatedAtRoute(nameof(HentBestillingByRefAsync), new {Referan = returBestilling.Id}, returBestilling);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditBestilling([FromBody] Bestilling bestilling, int id)
        {
            var dbBestilling = _db.HentEn(id).Result;

            var returBestilling = new Bestilling();
            if (dbBestilling != null)
            {
                returBestilling = await _db.Endre(id, bestilling);
            }
            else
            {
                return BadRequest($"No bestilling found with id {id}");
            }

            int lagre = await _db.Lagre();
            if (lagre > 0)
            {
                return Ok(returBestilling);
            }

            return BadRequest("No changes made");

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBestilling(int id)
        {
            if (await _db.Slett(id) >= 0)
            {
                return NoContent();
            }

            return BadRequest("No changes made");
        }

    }
}