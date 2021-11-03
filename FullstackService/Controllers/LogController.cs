using System.Threading.Tasks;
using FullstackService.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullstackService.Controllers
{
    [ApiController]
    [Route("api/log")]
    public class LogController: ControllerBase
    {
        private readonly ILogRepo _repo;
        private const string _loggetInn = "logget inn";

        public LogController(ILogRepo repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<ActionResult> HentAlleLogAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            return Ok(await _repo.HentLogAsync());
        }
    }
}