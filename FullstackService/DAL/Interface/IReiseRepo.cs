using System.Collections.Generic;
using System.Threading.Tasks;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public interface IReiseRepo
    {
        Task<bool> AddOneReiseAsync(Reise reise);

        Task<Reise> GetReiseByIdAsync(int id);

        Task<List<Reise>> GetAllReiseAsync();

        Task<Post> HentPoststedByPostnummer(string postnummer);

        Task<Reise> UpdateReiseAsync(int reiseId, Reise reise);

        Task<Reise> DeleteReiseAsync(int reiseId);

        Task<List<Bilde>> GetAlleBilder();
        Task InsertBilde(Bilde bilde);
    }
}