using System.Collections.Generic;
using System.Threading.Tasks;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public interface IBestillingRepo
    {

        Task<List<Bestilling>> HentAlleBestillingerAsync();

        Task<Bestilling> HentEnBestillingAsync(int id);

        Task<Bestilling> HentEnBestillingByRefAsync(string referanse);

        Task<Bestilling> LeggTilBestillingAsync(Bestilling bestilling);

        Task<Bestilling> EndreBestillingAsync(int id, Bestilling bestilling);
        
        Task<int> SlettBestillingAsync(int id);

        Task<int> LagreBestillingAsync();

    }
}