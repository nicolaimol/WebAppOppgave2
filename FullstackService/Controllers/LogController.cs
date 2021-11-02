using System.Threading.Tasks;
using FullstackService.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullstackService.Controllers
{
    [ApiController]
    [Route("api/log")]
    public class LogController: ControllerBase
    {
        private readonly ILogRepo _repo;

        public LogController(ILogRepo repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<ActionResult> HentAlleLogAsync()
        {
            return Ok(await _repo.HentLogAsync());
        }
    }
}