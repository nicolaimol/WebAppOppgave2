using System.Threading.Tasks;
using FullstackService.DAL;
using FullstackService.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullstackService.Controllers
{
    [ApiController]
    [Route("api/reise")]
    public class ReiserController: ControllerBase
    {
        private readonly IReiseRepo _repo;
        
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

        [HttpPost]
        public async Task<ActionResult> CreateOne([FromBody] Reise reise)
        {
            if ( await _repo.AddOne(reise))
            {
                return await CreateOne(reise);
            }

            return BadRequest("No reise created");
        }

        [HttpGet("lugar/{reiseId}")]
        public async Task<ActionResult> HentLugerByReise(int reiseId)
        {
            return Ok(await _repo.HentLugerByReise(reiseId));
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
        
    }
}