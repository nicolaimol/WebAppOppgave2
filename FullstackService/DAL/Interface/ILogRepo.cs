using System.Collections.Generic;
using System.Threading.Tasks;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public interface ILogRepo
    {
        Task LogAsync(string beskrivelse);
        Task<List<Log>> HentLogAsync();
    }
}