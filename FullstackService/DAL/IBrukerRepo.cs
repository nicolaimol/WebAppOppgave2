using System.Collections.Generic;
using System.Threading.Tasks;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public interface IBrukerRepo
    {
        Task<List<Bruker>> HentAlle();
        
        Task<Bruker> HentEn(int id);

        Task<Bruker> LeggTil(Bruker bruker);

        Task Endre(int id, Bruker bruker);

        Task<int> Slett(int id);

        Task<int> Lagre();
    }
}