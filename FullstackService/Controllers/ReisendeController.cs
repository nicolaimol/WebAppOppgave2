using System.Threading.Tasks;
using FullstackService.DAL;
using FullstackService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullstackService.Controllers
{
    [ApiController]
    [Route("api/reisende")]
    public class ReisendeController: ControllerBase
    {
        private readonly IReisendeRepo _repo;
        private const string _loggetInn = "logget inn";

        public ReisendeController(IReisendeRepo repo)
        {
            _repo = repo;
        }
        
        [HttpPut("kontaktperson/{id}")]
        public async Task<ActionResult> ChangeKontaktPerson(int id, KontaktPerson kontaktPerson)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            var returPerson = await _repo.ChangeKontaktPersonAsync(id, kontaktPerson);

            if (returPerson is null)
            {
                return BadRequest("Ingen person på id, eller ingen endringer");
            }
            
            return Ok(returPerson);
        }

        [HttpPut("voksen/{id}")]
        public async Task<ActionResult> ChangeVoksen(int id, Voksen voksen)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            var returPerson = await _repo.ChangeVoksenAsync(id, voksen);

            if (returPerson is null)
            {
                return BadRequest("Ingen person på id, eller ingen endringer");
            }
            
            return Ok(returPerson);
        }
        
        [HttpPut("barn/{id}")]
        public async Task<ActionResult> ChangeBarn(int id, Barn barn)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            var returPerson = await _repo.ChangeBarnAsync(id, barn);

            if (returPerson is null)
            {
                return BadRequest("Ingen person på id, eller ingen endringer");
            }
            
            return Ok(returPerson);
        }
    }
}